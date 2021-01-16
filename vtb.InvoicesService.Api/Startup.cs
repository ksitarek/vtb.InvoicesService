using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using vtb.InvoicesService.DataAccess;

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
            services.AddDbContext<InvoicesContext>(x => x.UseSqlServer(Configuration.GetConnectionString(nameof(InvoicesContext))));

            services.AddMassTransit(cfg =>
            {
                cfg.SetKebabCaseEndpointNameFormatter();

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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "vtb.InvoicesService", Version = "v1" });
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