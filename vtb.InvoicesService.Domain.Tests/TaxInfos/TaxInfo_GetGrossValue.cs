using NUnit.Framework;
using Shouldly;

namespace vtb.InvoicesService.Domain.Tests.TaxInfos
{
    public class TaxInfo_GetGrossValue
    {
        [TestCase(100, 0.23, CalculationDirection.NetToGross, 123)]
        [TestCase(123, 0.23, CalculationDirection.GrossToNet, 123)]
        public void Will_Return_Gross_Value(decimal value, decimal multiplier, CalculationDirection direction, decimal expected)
        {
            var taxInfo = new TaxInfo(string.Empty, multiplier);
            taxInfo.GetGrossValue(direction, value).ShouldBe(expected);
        }
    }
}
