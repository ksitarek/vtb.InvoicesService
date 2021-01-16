using System;

namespace vtb.InvoicesService.BusinessLogic.Events
{
    public interface IInvoicePrinted
    {
        Guid InvoiceId { get; }
        DateTime PrintoutDate { get; }
    }
}