using NUnit.Framework;
using Shouldly;
using System;

namespace vtb.InvoicesService.Domain.Tests.Invoices
{
    public class Invoice_Ctor : InvoiceTests
    {
        [TestCase("2000-05-05", "08837a2c-5463-4125-8b65-7672f753de9b", "a998263c-1c34-4bc4-b6a1-522c8c43f4e4", "29f588e0-0276-4ec6-99c9-0491fc93e343", "a4df41ff-d39c-4b43-a84d-22458ab901a0", Currency.EUR, CalculationDirection.GrossToNet)]
        [TestCase("2020-05-05", "19d95074-41c6-4551-aca1-350d2228b15c", "a998263c-1c34-4bc4-b6a1-522c8c43f4e4", "29f588e0-0276-4ec6-99c9-0491fc93e343", "a4df41ff-d39c-4b43-a84d-22458ab901a0", Currency.USD, CalculationDirection.NetToGross)]
        public void Will_Create_For_Valid_Input(string draftCreatedAtUtcString, Guid templateVersionId, Guid buyerId, Guid sellerId, Guid issuerId, Currency currency, CalculationDirection calculationDirection)
        {
            Should.NotThrow(() => new Invoice(
                DateTime.Parse(draftCreatedAtUtcString),
                templateVersionId,
                buyerId,
                sellerId,
                issuerId,
                currency,
                calculationDirection
            ));
        }

        [TestCase("future", "6483b1bc-f74f-43a1-a8e5-c3231ae552f6", "a998263c-1c34-4bc4-b6a1-522c8c43f4e4", "29f588e0-0276-4ec6-99c9-0491fc93e343", "a4df41ff-d39c-4b43-a84d-22458ab901a0", Currency.EUR, CalculationDirection.GrossToNet)]
        [TestCase("2020-05-05", "00000000-0000-0000-0000-000000000000", "a998263c-1c34-4bc4-b6a1-522c8c43f4e4", "29f588e0-0276-4ec6-99c9-0491fc93e343", "a4df41ff-d39c-4b43-a84d-22458ab901a0", Currency.USD, CalculationDirection.NetToGross)]
        [TestCase("2020-05-05", "aa950424-550d-4c6b-881a-f056643181c2", "00000000-0000-0000-0000-000000000000", "29f588e0-0276-4ec6-99c9-0491fc93e343", "a4df41ff-d39c-4b43-a84d-22458ab901a0", Currency.USD, CalculationDirection.NetToGross)]
        [TestCase("2020-05-05", "aa950424-550d-4c6b-881a-f056643181c2", "a998263c-1c34-4bc4-b6a1-522c8c43f4e4", "00000000-0000-0000-0000-000000000000", "a4df41ff-d39c-4b43-a84d-22458ab901a0", Currency.EUR, CalculationDirection.GrossToNet)]
        [TestCase("2020-05-05", "aa950424-550d-4c6b-881a-f056643181c2", "a998263c-1c34-4bc4-b6a1-522c8c43f4e4", "29f588e0-0276-4ec6-99c9-0491fc93e343", "00000000-0000-0000-0000-000000000000", Currency.EUR, CalculationDirection.GrossToNet)]
        [TestCase("2020-05-05", "aa950424-550d-4c6b-881a-f056643181c2", "a998263c-1c34-4bc4-b6a1-522c8c43f4e4", "29f588e0-0276-4ec6-99c9-0491fc93e343", "00000000-0000-0000-0000-000000000000", Currency.EUR, CalculationDirection.GrossToNet)]
        [TestCase("2020-05-05", "aa950424-550d-4c6b-881a-f056643181c2", "a998263c-1c34-4bc4-b6a1-522c8c43f4e4", "29f588e0-0276-4ec6-99c9-0491fc93e343", "a4df41ff-d39c-4b43-a84d-22458ab901a0", Currency.Unknown, CalculationDirection.GrossToNet)]
        [TestCase("2020-05-05", "aa950424-550d-4c6b-881a-f056643181c2", "a998263c-1c34-4bc4-b6a1-522c8c43f4e4", "29f588e0-0276-4ec6-99c9-0491fc93e343", "a4df41ff-d39c-4b43-a84d-22458ab901a0", Currency.EUR, CalculationDirection.Unknown)]
        public void Will_Not_Create_For_Invalid_Input(string draftCreatedAtUtcString, Guid templateVersionId, Guid buyerId, Guid sellerId, Guid issuerId, Currency currency, CalculationDirection calculationDirection)
        {
            if (draftCreatedAtUtcString == "future")
                draftCreatedAtUtcString = DateTime.UtcNow.AddMinutes(1).ToString();

            Should.Throw<ArgumentException>(() => new Invoice(
                DateTime.Parse(draftCreatedAtUtcString),
                templateVersionId,
                buyerId,
                sellerId,
                issuerId,
                currency,
                calculationDirection
            ));
        }
    }
}