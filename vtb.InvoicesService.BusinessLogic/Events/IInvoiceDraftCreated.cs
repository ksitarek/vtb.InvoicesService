using System;
using System.Collections.Generic;
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

    public interface IInvoiceIssued
    {
        Guid InvoiceId { get; }
        InvoiceNumber InvoiceNumber { get; }
        DateTime IssueDate { get; }
        Guid IssuerId { get; }
    }

    public interface IPaidInvoiceIssued
    {
        Guid InvoiceId { get; }
        InvoiceNumber InvoiceNumber { get; }
        DateTime IssueDate { get; }
        Guid IssuerId { get; }
    }

    public interface IInvoicePrinted
    {
        Guid InvoiceId { get; }
        DateTime PrintoutDate { get; }
    }

    public interface IInvoicePaid
    {
        Guid InvoiceId { get; }
        DateTime PaymentDate { get; }
    }

    public interface IInvoicePositionsSet
    {
        Guid InvoiceId { get; }
        IEnumerable<InvoicePosition> InvoicePositions { get; }
    }

    public interface IInvoiceTemplateSet
    {
        Guid InvoiceId { get; }
        Guid TemplateVersionId { get; }
    }
}