using System;
using System.Collections.Generic;
using vtb.InvoicesService.Domain;

namespace vtb.InvoicesService.BusinessLogic.Events
{
    public interface ICreateInvoiceDraft
    {
        Guid InvoiceId { get; }
        DateTime DraftCreatedAtUtc { get; }
        Guid TemplateVersionId { get; }
        Guid BuyerId { get; }
        Guid SellerId { get; }
        Currency Currency { get; }
        CalculationDirection CalculationDirection { get; }
    }

    public interface IIssueInvoice
    {
        Guid InvoiceId { get; }
        InvoiceNumber InvoiceNumber { get; }
        DateTime IssueDate { get; }
        Guid IssuerId { get; }
        DateTime PaymentDeadline { get; }
    }

    public interface IIssuePaidInvoice
    {
        Guid InvoiceId { get; }
        InvoiceNumber InvoiceNumber { get; }
        DateTime IssueDate { get; }
        Guid IssuerId { get; }
    }

    public interface IPrintInvoice
    {
        Guid InvoiceId { get; }
        DateTime PrintoutDate { get; }
    }

    public interface IPayInvoice
    {
        Guid InvoiceId { get; }
        DateTime PaymentDate { get; }
    }

    public interface ISetInvoicePositions
    {
        Guid InvoiceId { get; }
        IEnumerable<InvoicePosition> InvoicePositions { get; }
    }

    public interface ISetInvoiceTemplate
    {
        Guid InvoiceId { get; }
        Guid TemplateVersionId { get; }
    }

    public interface IInvoicePaymentDeadlineExpired
    {
        Guid InvoiceId { get; }
    }
}