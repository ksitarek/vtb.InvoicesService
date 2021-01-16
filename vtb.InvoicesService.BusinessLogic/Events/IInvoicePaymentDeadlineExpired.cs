using System;

namespace vtb.InvoicesService.BusinessLogic.Events
{
    public interface IInvoicePaymentDeadlineExpired
    {
        Guid InvoiceId { get; }
    }
}