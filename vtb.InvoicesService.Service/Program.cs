using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using vtb.InvoicesService.BusinessLogic.Consumers;

namespace vtb.InvoicesService.Service
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(cfg =>
                    {
                        cfg.SetKebabCaseEndpointNameFormatter();
                        cfg.AddConsumersFromNamespaceContaining<CreateInvoiceDraftConsumer>();

                        cfg.UsingRabbitMq((x, y) =>
                        {
                            var rmqConfig = hostContext.Configuration.GetSection("RabbitMq");

                            y.Host(rmqConfig.GetValue<string>("HostAddress"), rmqConfig.GetValue<string>("VirtualHost"), h =>
                            {
                                h.Username(rmqConfig.GetValue<string>("Username"));
                                h.Password(rmqConfig.GetValue<string>("Password"));
                            });

                            y.ConfigureEndpoints(x);
                        });
                    });

                    services.AddHostedService<InvoicesHostedService>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });

            if (isService)
                await builder.UseWindowsService().Build().RunAsync();
            else
                await builder.RunConsoleAsync();
        }
    }
}