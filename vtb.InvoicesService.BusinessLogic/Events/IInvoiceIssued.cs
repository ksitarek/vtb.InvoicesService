using System;
using vtb.InvoicesService.Domain;

namespace vtb.InvoicesService.BusinessLogic.Events
{
    public interface IInvoiceIssued
    {
        Guid InvoiceId { get; }
        InvoiceNumber InvoiceNumber { get; }
        DateTime IssueDate { get; }
        Guid IssuerId { get; }
        DateTime PaymentDeadline { get; }
    }
}