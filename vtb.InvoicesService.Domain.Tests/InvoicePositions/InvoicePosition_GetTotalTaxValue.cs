using NUnit.Framework;
using Shouldly;

namespace vtb.InvoicesService.Domain.Tests.InvoicePositions
{
    public class InvoicePosition_GetTotalTaxValue
    {
        [TestCase(0.23, 2, 100, CalculationDirection.NetToGross, 46)]
        [TestCase(0.23, 2, 123, CalculationDirection.GrossToNet, 46)]
        public void Will_Return_TaxValue(decimal taxMultiplier, decimal quantity, decimal value, CalculationDirection direction, decimal expected)
        {
            var taxInfo = new TaxInfo("test", taxMultiplier);
            var position = new InvoicePosition(1, "", quantity, taxInfo, value, string.Empty);
            position.GetTotalTaxValue(direction).ShouldBe(expected);
        }
    }
}