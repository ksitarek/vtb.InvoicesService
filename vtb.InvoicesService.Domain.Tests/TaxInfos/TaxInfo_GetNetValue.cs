using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vtb.InvoicesService.Domain.Tests.TaxInfos
{
    public class TaxInfo_GetNetValue
    {
        [TestCase(100, 0.23, CalculationDirection.NetToGross, 100)]
        [TestCase(123, 0.23, CalculationDirection.GrossToNet, 100)]
        public void Will_Return_Net_Value(decimal value, decimal multiplier, CalculationDirection direction, decimal expected)
        {
            var taxInfo = new TaxInfo("test", multiplier);
            taxInfo.GetNetValue(direction, value).ShouldBe(expected);
        }

        [Test]
        public void Will_Throw_When_CalculationDirection_Unknown()
        {
            var taxInfo = new TaxInfo("23%", 0.23m);
            Should.Throw<InvalidOperationException>(
                () => taxInfo.GetNetValue(CalculationDirection.Unknown, 123));
        }
    }
}
