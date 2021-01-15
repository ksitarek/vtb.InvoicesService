using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace vtb.InvoicesService.Domain.Tests.Invoices
{
    public class Invoice_TotalNetValue : InvoiceTests
    {
        [TestCaseSource(nameof(InvoiceTestCases))]
        public void Will_Sum_All_Net_Values_From_Positions((Invoice, decimal) input)
        {
            input.Item1.TotalNetValue.ShouldBe(input.Item2);
        }

        private static IEnumerable<(Invoice, decimal)> InvoiceTestCases
        {
            get
            {
                yield return (GenerateEmptyTestInvoice(CalculationDirection.NetToGross), 0m);
                yield return (GenerateEmptyTestInvoice(CalculationDirection.GrossToNet), 0m);

                yield return (GenerateTestInvoice(CalculationDirection.NetToGross, new[] { (23, 0.5m, 1m) }), 0.5m);
                yield return (GenerateTestInvoice(CalculationDirection.NetToGross, new[] { (23, 1m, 0.5m) }), 0.5m);
                yield return (GenerateTestInvoice(CalculationDirection.NetToGross, new[] { (23, 10m, 15m) }), 150m);
                yield return (GenerateTestInvoice(CalculationDirection.NetToGross, new[] { (23, 3m, 0.5m) }), 1.5m);
                yield return (GenerateTestInvoice(CalculationDirection.NetToGross, new[] { (23, 1m, 0.5m), (23, 1m, 0.5m) }), 1m);

                yield return (GenerateTestInvoice(CalculationDirection.GrossToNet, new[] { (23, 0.5m, 1m) }), 0.41m);
                yield return (GenerateTestInvoice(CalculationDirection.GrossToNet, new[] { (23, 1m, 0.5m) }), 0.41m);
                yield return (GenerateTestInvoice(CalculationDirection.GrossToNet, new[] { (23, 10m, 15m) }), 121.95m);
                yield return (GenerateTestInvoice(CalculationDirection.GrossToNet, new[] { (23, 3m, 0.5m) }), 1.22m);
                yield return (GenerateTestInvoice(CalculationDirection.GrossToNet, new[] { (23, 1m, 0.5m), (23, 1m, 0.5m) }), 0.82m);
            }
        }
    }
}