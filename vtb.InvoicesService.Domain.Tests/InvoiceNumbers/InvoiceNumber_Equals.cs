using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;

namespace vtb.InvoicesService.Domain.Tests.InvoiceNumbers
{
    public class InvoiceNumber_Equals
    {
        [Test]
        public void Will_Return_True_For_Same_FormattedNumber()
        {
            var formattedNumber = Guid.NewGuid().ToString();
            var number1 = new InvoiceNumber(1, 2, 3, 4, formattedNumber);
            var number2 = new InvoiceNumber(2, 3, 4, 5, formattedNumber);

            number1.Equals(number2).ShouldBeTrue();
            (number1 == number2).ShouldBeTrue();
        }

        [Test]
        public void Will_Return_True_For_Different_FormattedNumber()
        {
            var formattedNumber1 = Guid.NewGuid().ToString();
            var formattedNumber2 = Guid.NewGuid().ToString();
            var number1 = new InvoiceNumber(1, 2, 3, 4, formattedNumber1);
            var number2 = new InvoiceNumber(2, 3, 4, 5, formattedNumber2);

            number1.Equals(number2).ShouldBeFalse();
            (number1 == number2).ShouldBeFalse();
        }
    }
}
