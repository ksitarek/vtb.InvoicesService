using NUnit.Framework;
using Shouldly;
using System;

namespace vtb.InvoicesService.Domain.Tests.TaxInfos
{
    public class TaxInfo_GetGrossValue
    {
        [TestCase(100, 0.23, CalculationDirection.NetToGross, 123)]
        [TestCase(123, 0.23, CalculationDirection.GrossToNet, 123)]
        public void Will_Return_Gross_Value(decimal value, decimal multiplier, CalculationDirection direction, decimal expected)
        {
            var taxInfo = new TaxInfo("test", multiplier);
            taxInfo.GetGrossValue(direction, value).ShouldBe(expected);
        }

        [Test]
        public void Will_Throw_When_CalculationDirection_Unknown()
        {
            var taxInfo = new TaxInfo("23%", 0.23m);
            Should.Throw<InvalidOperationException>(
                () => taxInfo.GetGrossValue(CalculationDirection.Unknown, 100));
        }
    }
}