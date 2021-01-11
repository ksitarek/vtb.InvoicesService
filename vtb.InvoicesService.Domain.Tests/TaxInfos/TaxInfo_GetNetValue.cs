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
            var taxInfo = new TaxInfo(string.Empty, multiplier);
            taxInfo.GetNetValue(direction, value).ShouldBe(expected);
        }
    }
}
