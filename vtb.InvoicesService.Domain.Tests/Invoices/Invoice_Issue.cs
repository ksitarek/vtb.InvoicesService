using NUnit.Framework;
using Shouldly;
using System;

namespace vtb.InvoicesService.Domain.Tests.Invoices
{
    public class Invoice_Issue : InvoiceTests
    {
        [Test]
        public void Will_Set_Invoice_State_When_Issued()
        {
            var invoice = GenerateTestInvoice(CalculationDirection.NetToGross, new (int, decimal, decimal)[] { (23, 1, 100) });

            var issuedAt = DateTime.UtcNow;
            var invoiceNumber = new InvoiceNumber(1, issuedAt.Year, issuedAt.Month, issuedAt.Day, $"1/{issuedAt.Month}/{issuedAt.Year}");
            invoice.Issue(invoiceNumber, issuedAt);

            invoice.IssuedAtUtc.ShouldBe(issuedAt);
            invoice.InvoiceNumber.ShouldBe(invoiceNumber);
        }

        [Test]
        public void Will_Throw_When_Issued_Twice()
        {
            var invoice = GenerateTestInvoice(CalculationDirection.NetToGross, new (int, decimal, decimal)[] { (23, 1, 100) });

            var issuedAt = DateTime.UtcNow;
            var invoiceNumber = new InvoiceNumber(1, issuedAt.Year, issuedAt.Month, issuedAt.Day, $"1/{issuedAt.Month}/{issuedAt.Year}");
            invoice.Issue(invoiceNumber, issuedAt);

            Should.Throw<InvalidOperationException>(() => invoice.Issue(invoiceNumber, issuedAt))
                .Message.ShouldBe("Cannot issue invoice that already has been issued.");
        }

        [Test]
        public void Will_Throw_When_No_Positions()
        {
            var invoice = GenerateEmptyTestInvoice();

            var issuedAt = DateTime.UtcNow;
            var invoiceNumber = new InvoiceNumber(1, issuedAt.Year, issuedAt.Month, issuedAt.Day, $"1/{issuedAt.Month}/{issuedAt.Year}");
            Should.Throw<InvalidOperationException>(() => invoice.Issue(invoiceNumber, issuedAt));
        }
    }
}