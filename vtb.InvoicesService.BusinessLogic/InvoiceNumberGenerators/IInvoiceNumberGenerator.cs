using System;
using vtb.InvoicesService.Domain;

namespace vtb.InvoicesService.BusinessLogic.InvoiceNumberGenerators
{
    public interface IInvoiceNumberGenerator
    {
        InvoiceNumber GetNext(DateTime issueDate, InvoiceNumber lastInvoiceNumber);
    }
}