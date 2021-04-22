using GrpcNetCoreService.Configurations;
using GrpcNetCoreService.RabbitMq.Consumer;
using GrpcNetCoreService.RabbitMq.Producer;
using GrpcNetCoreService.Services;
using MassTransit;
using MassTransit.Definition;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace GrpcNetCoreService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();

            #region MassTransit

            services.AddHealthChecks();

            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);

            services.AddMassTransit(config =>
            {
                config.AddConsumer<OrderConsumer>();

                var rabbitMqSettings = Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();


                config.UsingRabbitMq((ctx, cfg) =>
                {

                    cfg.Host(rabbitMqSettings.Host);

                    cfg.ReceiveEndpoint("order-queue", action =>
                    {
                        action.ConfigureConsumer<OrderConsumer>(ctx);
                    });
                });
            });

            services.AddMassTransitHostedService();


            #region RabbitMQ dependencies
            services.AddScoped<IRabbitMqProducer, RabbitMqProducer>();
            #endregion

            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>();
            });
        }
    }
}
