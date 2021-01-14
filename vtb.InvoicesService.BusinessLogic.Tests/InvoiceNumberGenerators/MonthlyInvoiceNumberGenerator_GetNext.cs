using NUnit.Framework;
using Shouldly;
using System;
using vtb.InvoicesService.BusinessLogic.InvoiceNumberGenerators;
using vtb.InvoicesService.Domain;

namespace vtb.InvoicesService.BusinessLogic.Tests.InvoiceNumberGenerators
{
    public class MonthlyInvoiceNumberGenerator_GetNext
    {
        private MonthlyInvoiceNumberGenerator _generator;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _generator = new MonthlyInvoiceNumberGenerator();
        }

        [TestCase("2020-09-01", "", "00001/09/2020")]
        [TestCase("2020-10-01", "00001/09/2020", "00001/10/2020")]
        [TestCase("2020-10-01", "00004/10/2020", "00005/10/2020")]
        public void Will_Correctly_Generate_Next_Invoice_Number(string issueDateAsString, string oldNumberFormatted, string expectedNumberFormatted)
        {
            var issueDate = DateTime.Parse(issueDateAsString);

            var oldNumber = default(InvoiceNumber);

            if (!string.IsNullOrEmpty(oldNumberFormatted))
            {
                oldNumber = FormattedNumberToFirstDay(oldNumberFormatted);
            }

            var newNumber = _generator.GetNext(issueDate, oldNumber);

            newNumber.FormattedNumber.ShouldBe(expectedNumberFormatted);
            newNumber.OrderingNumber.ShouldBe(oldNumber != null ? FormattedNumberToFirstDay(expectedNumberFormatted).OrderingNumber : 1);
            newNumber.Year.ShouldBe(issueDate.Year);
            newNumber.Month.ShouldBe(issueDate.Month);
            newNumber.Day.ShouldBe(issueDate.Day);
        }

        [Test]
        public void Will_Throw_When_Last_Invoice_Number_Is_Max()
        {
            var issueDate = new DateTime(2020, 10, 1);
            var oldNumber = new InvoiceNumber(99999, 2020, 10, 1, "");

            Should.Throw<InvalidOperationException>(() => _generator.GetNext(issueDate, oldNumber));
        }

        [Test]
        public void Will_Throw_When_Last_Invoice_Number_Is_Newer_Than_Issue_Date()
        {
            var issueDate = new DateTime(2020, 10, 1);
            var oldNumber = new InvoiceNumber(3, 2020, 10, 2, "");

            Should.Throw<InvalidOperationException>(() => _generator.GetNext(issueDate, oldNumber));
        }

        private InvoiceNumber FormattedNumberToFirstDay(string oldNumberFormatted)
        {
            var splitted = oldNumberFormatted.Split("/");
            return new InvoiceNumber(
                int.Parse(splitted[0]),
                int.Parse(splitted[2]),
                int.Parse(splitted[1]),
                1,
                oldNumberFormatted);
        }
    }
}