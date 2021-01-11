using System;

namespace vtb.InvoicesService.Domain
{
    public class InvoiceNumber
    {
        public int OrderingNumber { get; private set; }

        public int Year { get; private set; }

        public int Month { get; private set; }

        public int Day { get; private set; }

        public string FormattedNumber { get; private set; }

        public DateTime IssuedAt { get => new DateTime(Year, Month, Day); }

        public InvoiceNumber(int orderingNumber, int year, int month, int day, string formattedNumber)
        {
            OrderingNumber = orderingNumber;
            Year = year;
            Month = month;
            Day = day;
            FormattedNumber = formattedNumber ?? throw new ArgumentNullException(nameof(formattedNumber));
        }
    }
}
