using System;

namespace vtb.InvoicesService.BusinessLogic.Events
{
    public interface IInvoicePaid
    {
        Guid InvoiceId { get; }
        DateTime PaymentDate { get; }
    }
}