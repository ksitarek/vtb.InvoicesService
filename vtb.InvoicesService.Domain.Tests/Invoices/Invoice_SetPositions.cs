using NUnit.Framework;
using Shouldly;
using System;

namespace vtb.InvoicesService.Domain.Tests.Invoices
{
    public class Invoice_SetPositions
    {
        [Test]
        public void Will_Set_Invoice_Positions()
        {
            var taxInfo = new TaxInfo("test", 0.23m);
            var invoice = new Invoice(DateTime.Today, Guid.Empty, Guid.Empty, Guid.Empty, Currency.EUR, CalculationDirection.NetToGross);
            var position1 = new InvoicePosition(1, "p1", 2, taxInfo, 100, "pc");
            var position3 = new InvoicePosition(3, "p2", 1, taxInfo, 400, "pc");
            var position2 = new InvoicePosition(2, "p2", 1, taxInfo, 400, "pc");

            invoice.SetPositions(new() { position1, position2, position3 });

            invoice.InvoicePositions.Count.ShouldBe(2);
            invoice.TotalNetValue.ShouldBe(600);
        }

        [Test]
        public void Will_Throw_For_Duplicated_Ordinal_Numbers()
        {
            var taxInfo = new TaxInfo("test", 0.23m);
            var invoice = new Invoice(DateTime.Today, Guid.Empty, Guid.Empty, Guid.Empty, Currency.EUR, CalculationDirection.NetToGross);
            var position1 = new InvoicePosition(1, "p1", 2, taxInfo, 100, "pc");
            var position2 = new InvoicePosition(1, "p2", 1, taxInfo, 400, "pc");

            Assert.Throws<InvalidOperationException>(() => invoice.SetPositions(new() { position1, position2 }));
        }

        [Test]
        public void Will_Throw_For_Ordinal_Numbers_Starting_From_Wrong_Value()
        {
            var taxInfo = new TaxInfo("test", 0.23m);
            var invoice = new Invoice(DateTime.Today, Guid.Empty, Guid.Empty, Guid.Empty, Currency.EUR, CalculationDirection.NetToGross);
            var position1 = new InvoicePosition(2, "p1", 2, taxInfo, 100, "pc");
            var position2 = new InvoicePosition(3, "p2", 1, taxInfo, 400, "pc");

            Assert.Throws<InvalidOperationException>(() => invoice.SetPositions(new() { position1, position2 }));
        }

        [Test]
        public void Will_Throw_For_Ordinal_Numbers_Not_In_Sequence()
        {
            var taxInfo = new TaxInfo("test", 0.23m);
            var invoice = new Invoice(DateTime.Today, Guid.Empty, Guid.Empty, Guid.Empty, Currency.EUR, CalculationDirection.NetToGross);
            var position1 = new InvoicePosition(1, "p1", 2, taxInfo, 100, "pc");
            var position2 = new InvoicePosition(3, "p2", 1, taxInfo, 400, "pc");

            Assert.Throws<InvalidOperationException>(() => invoice.SetPositions(new() { position1, position2 }));
        }
    }
}