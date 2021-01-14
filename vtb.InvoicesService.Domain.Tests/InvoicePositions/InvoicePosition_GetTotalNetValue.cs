using NUnit.Framework;
using Shouldly;

namespace vtb.InvoicesService.Domain.Tests.InvoicePositions
{
    public class InvoicePosition_GetTotalNetValue
    {
        private readonly TaxInfo _taxInfo = new TaxInfo("test", 0.23m);

        [Test]
        public void Will_Return_Value_When_Direction_Is_NetToGross()
        {
            var position = new InvoicePosition(1, "", 2, _taxInfo, 100, "");
            position.GetTotalNetValue(CalculationDirection.NetToGross).ShouldBe(200m);
        }

        [Test]
        public void Will_Return_Value_When_Direction_Is_GrossToNet()
        {
            var position = new InvoicePosition(1, "", 2, _taxInfo, 123, "");
            position.GetTotalNetValue(CalculationDirection.GrossToNet).ShouldBe(200m);
        }
    }
}