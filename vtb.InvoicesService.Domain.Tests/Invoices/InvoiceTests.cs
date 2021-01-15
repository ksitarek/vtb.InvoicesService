using System;
using System.Collections.Generic;

namespace vtb.InvoicesService.Domain.Tests.Invoices
{
    public abstract class InvoiceTests
    {
        protected static Invoice GenerateEmptyTestInvoice(CalculationDirection calculationDirection = CalculationDirection.NetToGross)
            => GenerateTestInvoice(calculationDirection, Array.Empty<(int, decimal, decimal)>());

        protected static Invoice GenerateTestInvoice(CalculationDirection direction, (int taxMultiplier, decimal quantity, decimal value)[] positionsSpec)
        {
            var invoice = new Invoice(DateTime.Today, new Guid("050367f9-b2dd-452e-9a4f-99fb2f0ede96"), new Guid("bcea44c9-77c2-4877-a373-3b94e7911464"), new Guid("bcea44c9-77c2-4877-a373-3b94e7911464"), new Guid("bcea44c9-77c2-4877-a373-3b94e7911464"), Currency.EUR, direction);
            var invoicePositions = new List<InvoicePosition>();
            for (var i = 0; i < positionsSpec.Length; i++)
            {
                var (taxMultiplier, quantity, value) = positionsSpec[i];
                var taxInfo = new TaxInfo(taxMultiplier.ToString(), taxMultiplier / 100m);
                invoicePositions.Add(new InvoicePosition(i + 1, "", quantity, taxInfo, value, "", ""));
            }

            invoice.SetPositions(invoicePositions);

            return invoice;
        }
    }
}