using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;

namespace vtb.InvoicesService.Domain
{
    public class Invoice
    {
        public Guid Id { get; private set; }

        public DateTime DraftCreatedAtUtc { get; }
        public DateTime? IssuedAtUtc { get; private set; }
        public DateTime? PaidAtUtc { get; private set; }

        public InvoiceNumber InvoiceNumber { get; private set; }

        public Guid BuyerId { get; }
        public Guid SellerId { get; }
        public Guid IssuerId { get; }

        public Currency Currency { get; }

        public CalculationDirection CalculationDirection { get; }

        public virtual List<InvoicePosition> InvoicePositions { get; } = new();

        public Invoice(DateTime draftCreatedAtUtc, Guid buyerId, Guid sellerId, Guid issuerId, Currency currency, CalculationDirection calculationDirection)
        {
            Ensure.That(draftCreatedAtUtc, nameof(draftCreatedAtUtc)).IsLte(DateTime.UtcNow);
            Ensure.That(buyerId, nameof(buyerId)).IsNotEmpty();
            Ensure.That(sellerId, nameof(sellerId)).IsNotEmpty();
            Ensure.That(issuerId, nameof(issuerId)).IsNotEmpty();

            if (currency == Currency.Unknown)
            {
                throw new ArgumentException(nameof(currency));
            }

            if (calculationDirection == CalculationDirection.Unknown)
            {
                throw new ArgumentException(nameof(calculationDirection));
            }

            DraftCreatedAtUtc = draftCreatedAtUtc;
            BuyerId = buyerId;
            SellerId = sellerId;
            IssuerId = issuerId;
            Currency = currency;
            CalculationDirection = calculationDirection;
        }

        public void SetPositions(List<InvoicePosition> positions)
        {
            if (IsIssued)
            {
                throw new InvalidOperationException("Cannot change positions of already issued invoice.");
            }

            if (positions.Any())
            {
                ValidateOrdinalNumbers(positions.Select(x => x.OrdinalNumber));
            }

            InvoicePositions.Clear();
            InvoicePositions.AddRange(positions);
        }

        private void ValidateOrdinalNumbers(IEnumerable<int> positions)
        {
            if (positions.Distinct().Count() != positions.Count())
            {
                throw new InvalidOperationException("At least one position has duplicated ordinal number.");
            }

            if (positions.Min() > 1)
            {
                throw new InvalidOperationException("Position ordinal numbers should start with 1.");
            }

            if (positions.Max() != positions.Count())
            {
                throw new InvalidOperationException("Position ordinal numbers are not sequential.");
            }
        }

        public void Issue(InvoiceNumber invoiceNumber, DateTime issuedAtUtc)
        {
            if (IsIssued)
            {
                throw new InvalidOperationException("Cannot issue invoice that already has been issued.");
            }

            if (InvoicePositions.Count == 0)
            {
                throw new InvalidOperationException("Cannot issue invoice that has no positions.");
            }

            InvoiceNumber = invoiceNumber;
            IssuedAtUtc = issuedAtUtc;
        }

        public bool IsIssued => IssuedAtUtc.HasValue;
        public bool IsPaid => PaidAtUtc.HasValue;

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
    }
}