using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using vtb.InvoicesService.BusinessLogic.Consumers;

namespace vtb.InvoicesService.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransit(cfg =>
            {
                cfg.SetKebabCaseEndpointNameFormatter();
                cfg.AddConsumersFromNamespaceContaining<CreateInvoiceDraftConsumer>();

                cfg.UsingRabbitMq((x, y) =>
                {
                    var rmqConfig = Configuration.GetSection("RabbitMq");

                    y.Host(rmqConfig.GetValue<string>("HostAddress"), rmqConfig.GetValue<string>("VirtualHost"), h =>
                    {
                        h.Username(rmqConfig.GetValue<string>("Username"));
                        h.Password(rmqConfig.GetValue<string>("Password"));
                    });

                    y.ConfigureEndpoints(x);
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "tmp", Version = "v1" });
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "tmp v1"));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}