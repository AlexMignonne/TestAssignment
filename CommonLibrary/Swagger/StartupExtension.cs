using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CommonLibrary.Swagger
{
    public static class StartupExtension
    {
        public static IServiceCollection AddSwagger(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var config = new SwaggerConfigModel();

            configuration
                .GetSection("Swagger")
                .Bind(config);

            if (!config.IsConfigured())
                throw new InvalidProgramException(
                    "Swagger not configured.");

            services
                .AddSwaggerGen(
                    _ =>
                    {
                        _.SwaggerDoc(
                            config.Name,
                            new OpenApiInfo
                            {
                                Title = config.Title,
                                Version = config.Version
                            });
                    });

            return services;
        }

        public static IApplicationBuilder UseSwagger(
            this IApplicationBuilder app,
            IConfiguration configuration)
        {
            var config = new SwaggerConfigModel();

            configuration
                .GetSection("Swagger")
                .Bind(config);

            if (!config.IsConfigured())
                throw new InvalidProgramException(
                    "Swagger not configured.");

            app
                .UseSwagger()
                .UseSwaggerUI(
                    _ =>
                    {
                        _.SwaggerEndpoint(
                            config.Url,
                            config.Name);

                        _.RoutePrefix = config.RoutePrefix;
                    });

            return app;
        }
    }
}
