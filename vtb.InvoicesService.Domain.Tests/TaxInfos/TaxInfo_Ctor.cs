using System;
using NUnit.Framework;
using Shouldly;

namespace vtb.InvoicesService.Domain.Tests.TaxInfos
{
    public class TaxInfo_Ctor
    {
        [TestCase(-0.1)]
        [TestCase(1)]
        public void Will_Not_Create_For_Invalid_Multiplier(decimal multiplier)
        {
            Should.Throw<ArgumentException>(() => new TaxInfo("test", multiplier));
        }

        [TestCase(0)]
        [TestCase(0.1)]
        public void Will_Create_For_Valid_Multiplier(decimal multiplier)
        {
            Should.NotThrow(() => new TaxInfo("test", multiplier));
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        public void Will_Not_Create_For_Invalid_Label(string label)
        {
            Should.Throw<ArgumentException>(() => new TaxInfo(label, 0.1m));
        }

        [TestCase("0")]
        [TestCase("zw")]
        [TestCase("23%")]
        public void Will_Create_For_Valid_Label(string label)
        {
            Should.NotThrow(() => new TaxInfo(label, 0.1m));
        }
    }
}