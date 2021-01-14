using NUnit.Framework;
using Shouldly;

namespace vtb.InvoicesService.Domain.Tests.InvoicePositions
{
    public class InvoicePosition_GetNetValue
    {
        private readonly TaxInfo _taxInfo = new TaxInfo("test", 0.23m);

        [Test]
        public void Will_Return_Value_When_Direction_Is_NetToGross()
        {
            var position = new InvoicePosition(1, "", 1, _taxInfo, 100, "");
            position.GetNetValue(CalculationDirection.NetToGross).ShouldBe(100m);
        }

        [Test]
        public void Will_Return_Value_When_Direction_Is_GrossToNet()
        {
            var position = new InvoicePosition(1, "", 1, _taxInfo, 123, "");
            position.GetNetValue(CalculationDirection.GrossToNet).ShouldBe(100m);
        }
    }
}