using Diversos.Core.Models.Email;
using Diversos.Infraestructure.Filters;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;

namespace Diversos.Infraestructure.Extensions.Services
{
    public static class ControllersServices
    {
        public static IServiceCollection AddControllersExtend(this IServiceCollection services, IConfiguration configuration)
        {
            // Establezco la configuracion que contiene los parametros para el envio de correos
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

            // Configuro los CORS
            var cors = configuration.GetValue<string[]>("CorsAllowed") ?? Array.Empty<string>();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsAllowed", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services
                .AddControllers(opt => opt.Filters.Add<GlobalValidationFilterAttribute>())

                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                    opt.UseCamelCasing(false);
                });

            services.AddFluentValidationAutoValidation();

            services.AddSwaggerGen();

            return services;
        }
    }
}
