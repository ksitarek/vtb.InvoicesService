using NUnit.Framework;
using Shouldly;

namespace vtb.InvoicesService.Domain.Tests.TaxInfos
{
    public class TaxInfo_Equals
    {
        [Test]
        public void Will_Return_False_When_TaxInfo_Compared_To_Null()
        {
            var taxInfo = new TaxInfo("23%", 0.23m);
            taxInfo.Equals(null).ShouldBeFalse();
            (taxInfo == null).ShouldBeFalse();
        }

        [Test]
        public void Will_Return_True_When_TaxInfo_Compared_To_Salf()
        {
            var taxInfo = new TaxInfo("23%", 0.23m);
            taxInfo.Equals(taxInfo).ShouldBeTrue();
        }

        [Test]
        public void Will_Return_True_When_Compared_The_Same_TaxInfo()
        {
            var taxInfo1 = new TaxInfo("23%", 0.23m);
            var taxInfo2 = new TaxInfo("23%", 0.23m);
            taxInfo1.Equals(taxInfo2).ShouldBeTrue();
            (taxInfo1 == taxInfo2).ShouldBeTrue();
        }

        [Test]
        public void Will_Return_False_When_Compared_The_Same_TaxInfo()
        {
            var taxInfo1 = new TaxInfo("0%", 0m);
            var taxInfo2 = new TaxInfo("zw", 0m);
            taxInfo1.Equals(taxInfo2).ShouldBeFalse();
            (taxInfo1 == taxInfo2).ShouldBeFalse();
        }
    }
}