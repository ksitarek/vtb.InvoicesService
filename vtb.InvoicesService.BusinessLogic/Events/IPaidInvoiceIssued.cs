using System;
using vtb.InvoicesService.Domain;

namespace vtb.InvoicesService.BusinessLogic.Events
{
    public interface IPaidInvoiceIssued
    {
        Guid InvoiceId { get; }
        InvoiceNumber InvoiceNumber { get; }
        DateTime IssueDate { get; }
        Guid IssuerId { get; }
    }
}