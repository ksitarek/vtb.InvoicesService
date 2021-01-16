using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vtb.InvoicesService.BusinessLogic.Events;
using vtb.InvoicesService.Domain;

namespace vtb.InvoicesService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public InvoicesController(
        IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("create-draft")]
        public async Task<IActionResult> CreateDraft()
        {
            var invoiceId = Guid.NewGuid();
            var draftCreatedAtUtc = DateTime.UtcNow;
            var templateVersionId = Guid.NewGuid();
            var buyerId = Guid.NewGuid();
            var sellerId = Guid.NewGuid();
            var currency = Currency.EUR;
            var calculationDirection = CalculationDirection.NetToGross;

            await _publishEndpoint.Publish<IInvoiceDraftCreated>(new
            {
                invoiceId,
                draftCreatedAtUtc,
                templateVersionId,
                buyerId,
                sellerId,
                currency,
                calculationDirection
            });

            return Accepted(invoiceId);
        }

        [HttpPost("add-pos")]
        public async Task<IActionResult> AddPos(Guid invoiceId)
        {
            var invoicePositions = new List<InvoicePosition>()
            {
                new InvoicePosition(1, "lipsum", 1, new TaxInfo("23%", 0.23m), 1000, "pc")
            };

            await _publishEndpoint.Publish<IInvoicePositionsSet>(new
            {
                invoiceId,
                invoicePositions
            });

            return Accepted();
        }

        [HttpPost("issue")]
        public async Task<IActionResult> Issue(Guid invoiceId)
        {
            var invoiceNumber = new InvoiceNumber(1, 2021, 1, 16, "");
            var issueDate = DateTime.UtcNow;
            var issuerId = Guid.NewGuid();
            var paymentDeadline = issueDate.AddDays(7);

            await _publishEndpoint.Publish<IInvoiceIssued>(new
            {
                invoiceId,
                invoiceNumber,
                issueDate,
                issuerId,
                paymentDeadline
            });

            return Accepted();
        }
    }
}