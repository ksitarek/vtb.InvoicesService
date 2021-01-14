using System;
using System.Security.Cryptography.Xml;

namespace vtb.InvoicesService.Domain
{
    public record InvoiceNumber
    {
        public int OrderingNumber { get; }

        public int Year { get; }

        public int Month { get; }

        public int Day { get; }

        public string FormattedNumber { get; }

        public DateTime IssuedAt { get => new DateTime(Year, Month, Day); }

        public InvoiceNumber(int orderingNumber, int year, int month, int day, string formattedNumber)
        {
            OrderingNumber = orderingNumber;
            Year = year;
            Month = month;
            Day = day;
            FormattedNumber = formattedNumber ?? throw new ArgumentNullException(nameof(formattedNumber));
        }

        public virtual bool Equals(InvoiceNumber other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return FormattedNumber.Equals(other.FormattedNumber);
        }

        public override int GetHashCode()
        {
            return FormattedNumber.GetHashCode();
        }
    }
}
