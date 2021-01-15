using NUnit.Framework;
using Shouldly;
using System;

namespace vtb.InvoicesService.Domain.Tests.Invoices
{
    public class Invoice_Issue : InvoiceTests
    {
        [Test]
        public void Will_Issue_Unpaid_Invoice()
        {
            var invoice = GenerateTestInvoice(CalculationDirection.NetToGross, new (int, decimal, decimal)[] { (23, 1, 100) });

            var issuedAt = DateTime.UtcNow;
            var invoiceNumber = new InvoiceNumber(1, issuedAt.Year, issuedAt.Month, issuedAt.Day, $"1/{issuedAt.Month}/{issuedAt.Year}");
            var issuerId = new Guid("d4068df8-3936-4601-aeb5-3530d0d8e407");
            invoice.Issue(invoiceNumber, issuedAt, issuerId);

            invoice.IssueDate.ShouldBe(issuedAt.Date);
            invoice.IssuerId.ShouldBe(issuerId);
            invoice.InvoiceNumber.ShouldBe(invoiceNumber);
            invoice.PaymentDate.ShouldBeNull();
        }

        [Test]
        public void Will_Throw_When_Issued_Twice()
        {
            var invoice = GenerateTestInvoice(CalculationDirection.NetToGross, new (int, decimal, decimal)[] { (23, 1, 100) });

            var issuedAt = DateTime.UtcNow;
            var invoiceNumber = new InvoiceNumber(1, issuedAt.Year, issuedAt.Month, issuedAt.Day, $"1/{issuedAt.Month}/{issuedAt.Year}");
            var issuerId = new Guid("d4068df8-3936-4601-aeb5-3530d0d8e407");
            invoice.Issue(invoiceNumber, issuedAt, issuerId);

            Should.Throw<InvalidOperationException>(() => invoice.Issue(invoiceNumber, issuedAt, issuerId))
                .Message.ShouldBe("Cannot issue invoice that already has been issued.");
        }

        [Test]
        public void Will_Throw_When_IssuerId_Empty()
        {
            var invoice = GenerateEmptyTestInvoice();

            var issuedAt = DateTime.UtcNow;
            var invoiceNumber = new InvoiceNumber(1, issuedAt.Year, issuedAt.Month, issuedAt.Day, $"1/{issuedAt.Month}/{issuedAt.Year}");
            var issuerId = Guid.Empty;

            Should.Throw<ArgumentException>(() => invoice.Issue(invoiceNumber, issuedAt, issuerId));
        }

        [Test]
        public void Will_Throw_When_No_Positions()
        {
            var invoice = GenerateEmptyTestInvoice();

            var issuedAt = DateTime.UtcNow;
            var invoiceNumber = new InvoiceNumber(1, issuedAt.Year, issuedAt.Month, issuedAt.Day, $"1/{issuedAt.Month}/{issuedAt.Year}");
            var issuerId = new Guid("d4068df8-3936-4601-aeb5-3530d0d8e407");

            Should.Throw<InvalidOperationException>(() => invoice.Issue(invoiceNumber, issuedAt, issuerId));
        }
    }
}