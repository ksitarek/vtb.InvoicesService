using System;
using System.Collections.Generic;
using System.Linq;

namespace vtb.InvoicesService.Domain
{
    public class Invoice
    {
        public Guid Id { get; private set; }

        public DateTime DraftCreatedAtUtc { get; private set; }
        public DateTime IssuedAtUtc { get; private set; }
        public DateTime PaidAtUtc { get; private set; }

        public InvoiceNumber InvoiceNumber { get; private set; }

        public Guid BuyerId { get; private set; }
        public Guid SellerId { get; private set; }
        public Guid IssuerId { get; private set; }

        public Currency Currency { get; private set; }

        public CalculationDirection CalculationDirection { get; private set; }

        public virtual ICollection<InvoicePosition> InvoicePositions { get; private set; } = new List<InvoicePosition>();

        public decimal TotalNetValue => InvoicePositions
            .Select(x => x.GetTotalNetValue(CalculationDirection))
            .DefaultIfEmpty(0m)
            .Sum();

        public decimal TotalGrossValue => InvoicePositions
            .Select(x => x.GetTotalGrossValue(CalculationDirection))
            .DefaultIfEmpty(0m)
            .Sum();

        public IEnumerable<TaxSummary> TaxSummaries => InvoicePositions
            .GroupBy(x => x.TaxInfo)
            .Select(x => new TaxSummary(
                x.Key.TaxLabel,
                x.Select(x => x.GetTotalNetValue(CalculationDirection)).DefaultIfEmpty(0).Sum(),
                x.Select(x => x.GetTotalGrossValue(CalculationDirection)).DefaultIfEmpty(0).Sum()
            ));

        public Invoice(DateTime draftCreatedAtUtc, Guid buyerId, Guid sellerId, Guid issuerId, Currency currency, CalculationDirection calculationDirection)
        {
            DraftCreatedAtUtc = draftCreatedAtUtc;
            BuyerId = buyerId;
            SellerId = sellerId;
            IssuerId = issuerId;
            Currency = currency;
            CalculationDirection = calculationDirection;
        }

        public void AddInvoicePosition(string summary, decimal quantity, TaxInfo taxInfo, decimal value, string unitOfMeasure, string description = "")
            => InvoicePositions.Add(new InvoicePosition(summary, quantity, taxInfo, value, unitOfMeasure, description));
    }
}