using AMAPP.API.Configurations;
using AMAPP.API.Data;
using AMAPP.API.Extensions;
using AMAPP.API.Middlewares;
using AMAPP.API.Models;
using AMAPP.API.Repository.CoproducerInfoRepository;
using AMAPP.API.Repository.DeliveryRepository;
using AMAPP.API.Repository.OrderRepository;
using AMAPP.API.Repository.ProducerInfoRepository;
using AMAPP.API.Repository.ProdutoRepository;
using AMAPP.API.Repository.ReservationRepository;
using AMAPP.API.Services.Implementations;
using AMAPP.API.Services.Interfaces;
using AMAPP.API.Utils;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;
using System.Reflection;
using System.Text;
using System.Threading.RateLimiting;

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

            // Configuração do Identity com validações de senha
            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                // Configurações básicas de senha
                options.Password.RequiredLength = 12; // Mínimo 12 caracteres
                options.Password.RequiredUniqueChars = 3; // Pelo menos 3 caracteres únicos
                options.Password.RequireNonAlphanumeric = true; // Requer caracteres especiais
                options.Password.RequireLowercase = true; // Requer minúsculas
                options.Password.RequireUppercase = true; // Requer maiúsculas
                options.Password.RequireDigit = true; // Requer dígitos
                
                // Configurações de conta
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false; // Ajuste conforme necessário

                // Configurações de lockout
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


            // ========== CONFIGURAÇÃO JWT SEGURA ==========
            builder.Services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // Exige HTTPS em produção
                options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
                //options.RequireHttpsMetadata = false;
                options.SaveToken = true; // Stores the token in the HttpContext.User

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"])),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ValidateIssuer = true, 
                    ValidateAudience = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],

                    ClockSkew = TimeSpan.Zero
                };

                // Add event hooks for debugging
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices
                            .GetRequiredService<ILogger<Program>>();
                        logger.LogWarning("JWT Authentication failed: {Error} for IP: {IP}",
                            context.Exception.Message,
                            context.HttpContext.Connection.RemoteIpAddress);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        var logger = context.HttpContext.RequestServices
                           .GetRequiredService<ILogger<Program>>();

                        var userName = context.Principal.FindFirst("name")?.Value ??
                                           context.Principal.Identity?.Name ??
                                           "Unknown";

                        logger.LogInformation("Token validated for user: {User}",
                            userName);
                        return Task.CompletedTask;
                    }
                };
            });


            // ========== CONFIGURAÇÃO DE AUTORIZAÇÃO COM POLÍTICAS ==========
            builder.Services.AddAuthorization(options =>
            {
                // Políticas específicas do AMAPP
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Administrator"));

                options.AddPolicy("ProducerOnly", policy =>
                    policy.RequireRole("Producer"));

                options.AddPolicy("CoproducerOnly", policy =>
                    policy.RequireRole("CoProducer"));

                options.AddPolicy("AmapOnly", policy =>
                    policy.RequireRole("Amap"));

                // Políticas de negócio
                options.AddPolicy("CanManageProducts", policy =>
                    policy.RequireRole("Producer", "Administrator"));

                options.AddPolicy("CanManageSubscriptions", policy =>
                    policy.RequireRole("CoProducer", "Administrator", "Amap"));

                options.AddPolicy("CanManagePayments", policy =>
                    policy.RequireRole("Administrator", "Amap"));

                options.AddPolicy("CanViewReports", policy =>
                    policy.RequireRole("Administrator", "Amap", "CoProducer"));
            });


            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.Configure<JwtSettings>(configuration.GetSection(key: nameof(JwtSettings)));
            builder.Services.Configure<EmailConfiguration>(configuration.GetSection(key: nameof(EmailConfiguration)));

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
            builder.Services.AddScoped<ICoproducerInfoRepository, CoproducerInfoRepository>();

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
                    opt.Window = TimeSpan.FromMinutes(1);    // Time window of 1 minute
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

            builder.Services.AddFluentValidationServices();
            builder.Services.AddSecurityValidationConfig();

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
                    if (builder.Environment.IsDevelopment())
                    {
                        // In development, allow all origins
                        policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    }
                    else
                    {
                        // Muito restritivo
                        policy.WithOrigins("https://amapp.com", "https://www.amapp.com")
                              .WithHeaders("Content-Type", "Authorization")
                              .WithMethods("GET", "POST", "PUT", "DELETE")
                              .AllowCredentials();
                    }
                    
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

            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }
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