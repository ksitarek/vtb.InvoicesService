using System;
using System.Collections.Generic;
using vtb.InvoicesService.Domain;

namespace vtb.InvoicesService.BusinessLogic.Events
{
    public interface IInvoicePositionsSet
    {
        Guid InvoiceId { get; }
        IEnumerable<InvoicePosition> InvoicePositions { get; }
    }
}