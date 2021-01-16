using System;

namespace vtb.InvoicesService.BusinessLogic.Events
{
    public interface IInvoiceTemplateSet
    {
        Guid InvoiceId { get; }
        Guid TemplateVersionId { get; }
    }
}