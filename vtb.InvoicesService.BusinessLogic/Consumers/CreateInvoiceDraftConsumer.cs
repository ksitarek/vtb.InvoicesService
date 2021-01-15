using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using vtb.InvoicesService.BusinessLogic.Events;

namespace vtb.InvoicesService.BusinessLogic.Consumers
{
    public class CreateInvoiceDraftConsumer : IConsumer<ICreateInvoiceDraft>
    {
        private readonly ILogger<CreateInvoiceDraftConsumer> _logger;

        public CreateInvoiceDraftConsumer(ILogger<CreateInvoiceDraftConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<ICreateInvoiceDraft> context)
        {
            _logger.LogDebug(this.GetType().FullName);
            return Task.CompletedTask;
        }
    }
}