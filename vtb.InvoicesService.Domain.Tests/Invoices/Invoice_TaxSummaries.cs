using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace vtb.InvoicesService.Domain.Tests.Invoices
{
    public class Invoice_TaxSummaries : InvoiceTests
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
                yield return (GenerateEmptyTestInvoice(CalculationDirection.NetToGross), new List<TaxSummary>());
                yield return (GenerateEmptyTestInvoice(CalculationDirection.GrossToNet), new List<TaxSummary>());

                yield return (GenerateTestInvoice(CalculationDirection.NetToGross, new[]
                {
                    (23, 2m, 50m),
                    (7, 1m, 50m),
                    (7, 1m, 50m),
                }), new List<TaxSummary>() {
                    new TaxSummary("23", 100, 123),
                    new TaxSummary("7", 100, 107),
                });

                yield return (GenerateTestInvoice(CalculationDirection.GrossToNet, new[]
                {
                    (23, 2m, 62m),
                    (7, 1m, 50m),
                    (7, 1m, 50m),
                }), new List<TaxSummary>() {
                    new TaxSummary("23", 100.81m, 124),
                    new TaxSummary("7", 93.46m, 100),
                });
            }
        }
    }
}