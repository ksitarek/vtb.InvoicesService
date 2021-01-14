using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace vtb.InvoicesService.Domain.Tests.Invoices
{
    public class Invoice_TotalGrossValue : InvoiceTests
    {
        [TestCaseSource(nameof(InvoiceTestCases))]
        public void Will_Sum_All_Gross_Values_From_Positions((Invoice, decimal) input)
        {
            input.Item1.TotalGrossValue.ShouldBe(input.Item2);
        }

        private static IEnumerable<(Invoice, decimal)> InvoiceTestCases
        {
            get
            {
                yield return (GenerateEmptyTestInvoice(CalculationDirection.NetToGross), 0m);
                yield return (GenerateEmptyTestInvoice(CalculationDirection.GrossToNet), 0m);

                yield return (GenerateTestInvoice(CalculationDirection.NetToGross, new[] { (23, 0.5m, 1m) }), 0.62m);
                yield return (GenerateTestInvoice(CalculationDirection.NetToGross, new[] { (23, 1m, 0.5m) }), 0.62m);
                yield return (GenerateTestInvoice(CalculationDirection.NetToGross, new[] { (23, 10m, 15m) }), 184.5m);
                yield return (GenerateTestInvoice(CalculationDirection.NetToGross, new[] { (23, 3m, 0.5m) }), 1.84m);
                yield return (GenerateTestInvoice(CalculationDirection.NetToGross, new[] { (23, 1m, 0.5m), (23, 1m, 0.5m) }), 1.24m);

                yield return (GenerateTestInvoice(CalculationDirection.GrossToNet, new[] { (23, 0.5m, 1m) }), 0.5m);
                yield return (GenerateTestInvoice(CalculationDirection.GrossToNet, new[] { (23, 1m, 0.5m) }), 0.5m);
                yield return (GenerateTestInvoice(CalculationDirection.GrossToNet, new[] { (23, 10m, 15m) }), 150m);
                yield return (GenerateTestInvoice(CalculationDirection.GrossToNet, new[] { (23, 3m, 0.5m) }), 1.5m);
                yield return (GenerateTestInvoice(CalculationDirection.GrossToNet, new[] { (23, 1m, 0.5m), (23, 1m, 0.5m) }), 1m);
            }
        }
    }
}