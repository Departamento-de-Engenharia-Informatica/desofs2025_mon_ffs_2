using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace AMAPP.API.Extensions
{
    public static class ValidatorServiceExtensions
    {
        /// <summary>
        /// Registra todos os validators do FluentValidation
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            // Configuração básica do FluentValidation
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            // Registrar todos os validators automaticamente
            // Isso vai encontrar todos os validators em todos os assemblies
            services.AddValidatorsFromAssemblyContaining<Program>();

            // Ou registrar por namespace específico (mais performático)
            /*
            services.AddValidatorsFromAssemblyContaining<ChangePasswordDtoValidator>(); // Auth
            services.AddValidatorsFromAssemblyContaining<CreateDeliveryDtoValidator>(); // Delivery  
            services.AddValidatorsFromAssemblyContaining<CreateOrderDTOValidator>(); // Order
            */

            // Configurar resposta customizada para erros de validação
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();

                    return new BadRequestObjectResult(new
                    {
                        message = "Validation failed",
                        errors = errors,
                        timestamp = DateTime.UtcNow
                    });
                };
            });

            return services;
        }

        /// <summary>
        /// Adiciona configurações específicas de segurança para validação
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSecurityValidationConfig(this IServiceCollection services)
        {
            // Configurações de segurança para upload de arquivos
            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = 1024 * 1024; // 1MB para valores de form
                options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10MB para multipart
                options.MemoryBufferThreshold = 1024 * 1024; // 1MB buffer
            });

            return services;
        }
    }
}
