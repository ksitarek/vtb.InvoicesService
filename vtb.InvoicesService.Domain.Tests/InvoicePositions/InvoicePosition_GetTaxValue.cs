using NUnit.Framework;
using Shouldly;

namespace vtb.InvoicesService.Domain.Tests.InvoicePositions
{
    public class InvoicePosition_GetTaxValue
    {
        [TestCase(0.23, 100, CalculationDirection.NetToGross, 23)]
        [TestCase(0.23, 123, CalculationDirection.GrossToNet, 23)]
        public void Will_Return_TaxValue(decimal taxMultiplier, decimal value, CalculationDirection direction, decimal expected)
        {
            var taxInfo = new TaxInfo("test", taxMultiplier);
            var position = new InvoicePosition(1, "", 1, taxInfo, value, string.Empty);
            position.GetTaxValue(direction).ShouldBe(expected);
        }
    }
}