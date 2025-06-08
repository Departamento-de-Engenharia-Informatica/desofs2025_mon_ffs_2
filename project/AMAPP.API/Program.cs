using AMAPP.API.Configurations;
using AMAPP.API.Data;
using AMAPP.API.DTOs.CompoundProductProduct.Validators;
using AMAPP.API.DTOs.SelectedProductOffer.Validators;
using AMAPP.API.DTOs.SubscriptionPayment.Validators;
using AMAPP.API.DTOs.SubscriptionPeriod.Validators;
using AMAPP.API.Middlewares;
using AMAPP.API.Models;
using AMAPP.API.Repository.DeliveryRepository;
using AMAPP.API.Repository.OrderRepository;
using AMAPP.API.Repository.ProducerInfoRepository;
using AMAPP.API.Repository.ProdutoRepository;
using AMAPP.API.Services.Implementations;
using AMAPP.API.Services.Interfaces;
using AMAPP.API.Utils;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using AMAPP.API.Repository.ReservationRepository;
using QuestPDF.Infrastructure;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Microsoft.Extensions.Logging;

namespace AMAPP.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            QuestPDF.Settings.License = LicenseType.Community;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            var configuration = builder.Configuration;

            // Add services to the container.

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection")));

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false; // Alterar para true se for necess�ria confirma��o por email
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            builder.Services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true; // Stores the token in the HttpContext.User
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"])),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                // Add event hooks for debugging
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine($"Token validated successfully for user: {context.Principal.Identity?.Name}");
                        return Task.CompletedTask;
                    }
                };
            });



            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.Configure<JwtSettings>(configuration.GetSection(key: nameof(JwtSettings)));
            builder.Services.Configure<EmailConfiguration>(configuration.GetSection(key: nameof(EmailConfiguration)));

            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<TokenService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProducerInfoRepository, ProducerInfoRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IDeliveryService, DeliveryService>();
            builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            builder.Services.AddScoped<IReportService, ReportService>();

            builder.Services.AddRouting(options =>
            {
                options.LowercaseUrls = true; // Ensure URLs are lowercase
                options.LowercaseQueryStrings = true; // Optional: lowercase query strings
                options.ConstraintMap["kebab"] = typeof(KebabCaseParameterTransformer); // Register transformer
            });

            builder.Services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseParameterTransformer()));
            });


            // Configure rate limiting
            builder.Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddFixedWindowLimiter("FixedPolicy", opt =>
                {
                    opt.Window = TimeSpan.FromSeconds(1);    // Time window of 1 minute
                    opt.PermitLimit = 100;                   // Allow 100 requests per minute
                    opt.QueueLimit = 2;                      // Queue limit of 2
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });

                // TODO: try to log the rate limit rejection
                //options.OnRejected = async (context, cancellationToken) =>
                //{
                //    // Custom rejection handling logic
                //    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                //    context.HttpContext.Response.Headers["Retry-After"] = "60";

                //    await context.HttpContext.Response.WriteAsync("Rate limit exceeded. Please try again later.", cancellationToken);

                //    // Optional logging
                //    Console.WriteLine("Rate limit exceeded for IP: {IpAddress}",
                //        context.HttpContext.Connection.RemoteIpAddress);
                //};
            });


            builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateSelectedProductOfferDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateSubscriptionPeriodDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateSubscriptionPaymentDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateCompoundProductProductDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateCompoundProductProductDtoValidator>();
            
            // Add MediatR
            builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "AMAPP API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });


            var app = builder.Build();


            // Seed roles and users
            await DatabaseSeed.SeedRolesAndUsers(app.Services);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseRouting();
            app.UseRateLimiter();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers().RequireRateLimiting("FixedPolicy");


            app.Run();
        }
    }
}