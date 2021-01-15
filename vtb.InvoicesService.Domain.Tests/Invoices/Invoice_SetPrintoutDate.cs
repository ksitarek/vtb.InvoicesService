using System;
using NUnit.Framework;
using Shouldly;

namespace vtb.InvoicesService.Domain.Tests.Invoices
{
    public class Invoice_SetPrintoutDate : InvoiceTests
    {
        [Test]
        public void Will_Set_PrintoutDate()
        {
            var invoice = GenerateTestInvoice(CalculationDirection.NetToGross, new[] { (23, 1m, 1m) });
            invoice.Issue(new InvoiceNumber(1, 1, 1, 1, ""), DateTime.UtcNow);

            var printoutDateTime = DateTime.UtcNow;
            invoice.SetPrintoutDate(printoutDateTime);

            invoice.PrintoutDate.ShouldBe(printoutDateTime.Date);
        }

        [Test]
        public void Will_Not_Set_PrintoutDate_If_Invoice_Not_Issued()
        {
            var invoice = GenerateTestInvoice(CalculationDirection.NetToGross, new[] { (23, 1m, 1m) });

            var printoutDateTime = DateTime.UtcNow;
            Should.Throw<InvalidOperationException>(() => invoice.SetPrintoutDate(printoutDateTime));
        }

        [Test]
        public void Will_Not_Set_PrintoutDate_If_Date_Before_IssueDate()
        {
            var invoice = GenerateTestInvoice(CalculationDirection.NetToGross, new[] { (23, 1m, 1m) });
            invoice.Issue(new InvoiceNumber(1, 1, 1, 1, ""), DateTime.UtcNow);

            var printoutDateTime = invoice.IssueDate.Value.AddDays(-1);
            Should.Throw<InvalidOperationException>(() => invoice.SetPrintoutDate(printoutDateTime));
        }
    }
}