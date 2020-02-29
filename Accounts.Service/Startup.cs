using Accounts.Api.Extensions;
using Accounts.Application.UseCases.Extensions;
using Accounts.Infrastructure.Extensions;
using Accounts.Service.Middlewares;
using CommonLibrary.RedisCache;
using CommonLibrary.RequestInfo;
using CommonLibrary.Swagger;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StartupExtension = Accounts.Application.UseCases.Extensions.StartupExtension;

namespace Accounts.Service
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(
            IServiceCollection services)
        {
            services
                .AddMediatR(
                    GetType()
                        .Assembly,
                    typeof(StartupExtension)
                        .Assembly,
                    typeof(Infrastructure.Extensions.StartupExtension)
                        .Assembly,
                    typeof(Api.Extensions.StartupExtension)
                        .Assembly
                )
                .AddRedisCache(
                    Configuration)
                .AddApplicationServices()
                .AddInfrastructureServices(
                    Configuration)
                .AddApiServices(
                    Configuration)
                .AddSwagger(
                    Configuration)
                .AddControllers()
                .AddApplicationPart(
                    typeof(Api.Extensions.StartupExtension)
                        .Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //    app.UseDeveloperExceptionPage();

            app
                //.UseHttpsRedirection()
                .UseRouting()
                .UseMiddleware<ExceptionMiddleware>()
                .UseRequestInfo()
                .UseAuthorization()
                .UseSwagger(
                    Configuration)
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app
                .ApplicationServices
                .ApplyMigration();
        }
    }
}
