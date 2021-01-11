using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;

namespace vtb.InvoicesService.Domain.Tests.Invoices
{
    public class Invoice_TaxSummaries
    {
        [TestCaseSource(nameof(InvoiceTestCases))]
        public void Will_Calculate_Tax_Summaries((Invoice, IEnumerable<TaxSummary>) input)
        {
            input.Item1.TaxSummaries.ToList().ShouldBeEquivalentTo(input.Item2);
        }

        private static IEnumerable<(Invoice, IEnumerable<TaxSummary>)> InvoiceTestCases
        {
            get
            {
                yield return (GenerateTestInvoice(CalculationDirection.NetToGross, new (int, decimal, decimal)[0]), new List<TaxSummary>());
                yield return (GenerateTestInvoice(CalculationDirection.GrossToNet, new (int, decimal, decimal)[0]), new List<TaxSummary>());

                yield return (GenerateTestInvoice(CalculationDirection.NetToGross, new[]
                {
                    (23, 2m, 50m),
                    (7, 1m, 50m),
                    (7, 1m, 50m),
                }), new List<TaxSummary>() {
                    new TaxSummary("23%", 100, 123),
                    new TaxSummary("7%", 100, 107),
                });

                yield return (GenerateTestInvoice(CalculationDirection.GrossToNet, new[]
                {
                    (23, 2m, 62m),
                    (7, 1m, 50m),
                    (7, 1m, 50m),
                }), new List<TaxSummary>() {
                    new TaxSummary("23%", 100.81m, 124),
                    new TaxSummary("7%", 93.46m, 100),
                });
            }
        }

        private static Invoice GenerateTestInvoice(CalculationDirection direction, (int TaxMultiplier, decimal Quantity, decimal Value)[] positionsSpec)
        {
            var invoice = new Invoice(DateTime.Today, Guid.Empty, Guid.Empty, Guid.Empty, Currency.EUR, direction);
            for (var i = 0; i < positionsSpec.Length; i++)
            {
                var posSpec = positionsSpec[i];
                var taxInfo = new TaxInfo($"{posSpec.TaxMultiplier}%", posSpec.TaxMultiplier / 100m);
                invoice.AddInvoicePosition("", posSpec.Quantity, taxInfo, posSpec.Value, "", "");
            }

            return invoice;
        }
    }
}