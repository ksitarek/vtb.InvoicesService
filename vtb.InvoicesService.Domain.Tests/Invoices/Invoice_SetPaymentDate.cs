using NUnit.Framework;
using Shouldly;
using System;

namespace vtb.InvoicesService.Domain.Tests.Invoices
{
    public class Invoice_SetPaymentDate : InvoiceTests
    {
        [Test]
        public void Will_Set_PaymentDate()
        {
            var invoice = GenerateTestInvoice(CalculationDirection.NetToGross, new[] { (23, 1m, 1m) });
            invoice.Issue(new InvoiceNumber(1, 1, 1, 1, ""), DateTime.UtcNow, Guid.NewGuid());

            var paymentDateTime = DateTime.UtcNow;
            invoice.SetPaymentDate(paymentDateTime);

            invoice.PaymentDate.ShouldBe(paymentDateTime.Date);
        }

        [Test]
        public void Will_Not_Set_PaymentDate_If_Invoice_Not_Issued()
        {
            var invoice = GenerateTestInvoice(CalculationDirection.NetToGross, new[] { (23, 1m, 1m) });

            var paymentDateTime = DateTime.UtcNow;
            Should.Throw<InvalidOperationException>(() => invoice.SetPaymentDate(paymentDateTime));
        }

        [Test]
        public void Will_Not_Set_PaymentDate_If_Date_Before_IssueDate()
        {
            var invoice = GenerateTestInvoice(CalculationDirection.NetToGross, new[] { (23, 1m, 1m) });
            invoice.Issue(new InvoiceNumber(1, 1, 1, 1, ""), DateTime.UtcNow, Guid.NewGuid());

            var paymentDateTime = invoice.IssueDate.Value.AddDays(-1);
            Should.Throw<InvalidOperationException>(() => invoice.SetPaymentDate(paymentDateTime));
        }
    }
}