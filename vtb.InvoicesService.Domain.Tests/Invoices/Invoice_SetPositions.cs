using NUnit.Framework;
using Shouldly;
using System;

namespace vtb.InvoicesService.Domain.Tests.Invoices
{
    public class Invoice_SetPositions : InvoiceTests
    {
        [Test]
        public void Will_Set_Invoice_Positions()
        {
            var invoice = GenerateEmptyTestInvoice();

            var taxInfo = new TaxInfo("test", 0.23m);
            var position1 = new InvoicePosition(1, "p1", 2, taxInfo, 100, "pc");
            var position3 = new InvoicePosition(3, "p2", 1, taxInfo, 400, "pc");
            var position2 = new InvoicePosition(2, "p2", 1, taxInfo, 100, "pc");

            invoice.SetPositions(new() { position1, position2, position3 });

            invoice.InvoicePositions.Count.ShouldBe(3);
            invoice.TotalNetValue.ShouldBe(700);
        }

        [Test]
        public void Will_Throw_For_Duplicated_Ordinal_Numbers()
        {
            var invoice = GenerateEmptyTestInvoice();

            var taxInfo = new TaxInfo("test", 0.23m);
            var position1 = new InvoicePosition(1, "p1", 2, taxInfo, 100, "pc");
            var position2 = new InvoicePosition(1, "p2", 1, taxInfo, 400, "pc");

            Assert.Throws<InvalidOperationException>(() => invoice.SetPositions(new() { position1, position2 }));
        }

        [Test]
        public void Will_Throw_For_Ordinal_Numbers_Starting_From_Wrong_Value()
        {
            var invoice = GenerateEmptyTestInvoice();

            var taxInfo = new TaxInfo("test", 0.23m);
            var position1 = new InvoicePosition(2, "p1", 2, taxInfo, 100, "pc");
            var position2 = new InvoicePosition(3, "p2", 1, taxInfo, 400, "pc");

            Assert.Throws<InvalidOperationException>(() => invoice.SetPositions(new() { position1, position2 }));
        }

        [Test]
        public void Will_Throw_For_Ordinal_Numbers_Not_In_Sequence()
        {
            var invoice = GenerateEmptyTestInvoice();

            var taxInfo = new TaxInfo("test", 0.23m);
            var position1 = new InvoicePosition(1, "p1", 2, taxInfo, 100, "pc");
            var position2 = new InvoicePosition(3, "p2", 1, taxInfo, 400, "pc");

            Assert.Throws<InvalidOperationException>(() => invoice.SetPositions(new() { position1, position2 }));
        }
    }
}