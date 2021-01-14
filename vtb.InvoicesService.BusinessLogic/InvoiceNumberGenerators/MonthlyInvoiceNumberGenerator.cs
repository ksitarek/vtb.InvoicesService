using System;
using vtb.InvoicesService.Domain;

namespace vtb.InvoicesService.BusinessLogic.InvoiceNumberGenerators
{
    public class MonthlyInvoiceNumberGenerator : IInvoiceNumberGenerator
    {
        private const int MaxOrderingNumber = 99999;
        private int MaxOrderingNumberLen => MaxOrderingNumber.ToString().Length;

        public InvoiceNumber GetNext(DateTime issueDate, InvoiceNumber lastInvoiceNumber)
        {
            if (lastInvoiceNumber != null)
            {
                if (lastInvoiceNumber.OrderingNumber >= MaxOrderingNumber)
                {
                    throw new InvalidOperationException(
                        $"Cannot generate new unique number greater than {MaxOrderingNumber}");
                }

                if (issueDate < lastInvoiceNumber.IssuedAt)
                {
                    throw new InvalidOperationException("Cannot generate new number for older date.");
                }
            }

            var newOrderingNumber = lastInvoiceNumber == null || !SameMonth(issueDate, lastInvoiceNumber)
                ? 1
                : lastInvoiceNumber.OrderingNumber + 1;

            return new InvoiceNumber(newOrderingNumber,
                issueDate.Year,
                issueDate.Month,
                issueDate.Day,
                $"{PadInt(newOrderingNumber, MaxOrderingNumberLen)}/{PadInt(issueDate.Month, 2)}/{issueDate.Year}");
        }

        private string PadInt(int val, int len)
        {
            return val.ToString()
                .PadLeft(len, '0');
        }

        private bool SameMonth(DateTime issueDate, InvoiceNumber invoiceNumber)
        {
            return issueDate.Year == invoiceNumber.Year
                   && issueDate.Month == invoiceNumber.Month;
        }
    }
}