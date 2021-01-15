using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using vtb.InvoicesService.BusinessLogic;
using vtb.InvoicesService.BusinessLogic.Events;
using vtb.InvoicesService.Domain;

namespace vtb.InvoicesService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public InvoicesController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var invoiceId = Guid.NewGuid();
            var draftCreatedAtUtc = DateTime.UtcNow;
            var templateVersionId = Guid.NewGuid();
            var buyerId = Guid.NewGuid();
            var sellerId = Guid.NewGuid();
            var currency = Currency.EUR;
            var calculationDirection = CalculationDirection.NetToGross;

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(Endpoints.CreateInvoiceDraft);
            await endpoint.Send<ICreateInvoiceDraft>(new
            {
                invoiceId,
                draftCreatedAtUtc,
                templateVersionId,
                buyerId,
                sellerId,
                currency,
                calculationDirection
            });

            return Accepted();
        }
    }
}