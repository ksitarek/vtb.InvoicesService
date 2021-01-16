using System;
using vtb.InvoicesService.Domain;

namespace vtb.InvoicesService.BusinessLogic.Events
{
    public interface IInvoiceDraftCreated
    {
        Guid InvoiceId { get; }
        DateTime DraftCreatedAtUtc { get; }
        Guid TemplateVersionId { get; }
        Guid BuyerId { get; }
        Guid SellerId { get; }
        Currency Currency { get; }
        CalculationDirection CalculationDirection { get; }
    }
}