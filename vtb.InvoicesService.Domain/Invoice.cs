using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;

namespace vtb.InvoicesService.Domain
{
    public class Invoice
    {
        public Guid Id { get; private set; }
        public Guid TemplateVersionId { get; private set; }
        public DateTime DraftCreatedAtUtc { get; }
        public DateTime? IssueDate { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public DateTime? PrintoutDate { get; private set; }
        public InvoiceNumber InvoiceNumber { get; private set; }
        public Guid BuyerId { get; }
        public Guid SellerId { get; }
        public Guid IssuerId { get; }
        public Currency Currency { get; }
        public CalculationDirection CalculationDirection { get; }
        public virtual List<InvoicePosition> InvoicePositions { get; } = new();

        public Invoice(DateTime draftCreatedAtUtc, Guid templateVersionId, Guid buyerId, Guid sellerId, Guid issuerId,
            Currency currency, CalculationDirection calculationDirection)
        {
            Ensure.That(draftCreatedAtUtc, nameof(draftCreatedAtUtc)).IsLte(DateTime.UtcNow);
            Ensure.That(templateVersionId, nameof(templateVersionId)).IsNotEmpty();
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
            TemplateVersionId = templateVersionId;
            BuyerId = buyerId;
            SellerId = sellerId;
            IssuerId = issuerId;
            Currency = currency;
            CalculationDirection = calculationDirection;
        }

        public void Issue(InvoiceNumber invoiceNumber, DateTime issuedAtUtc, bool isPaid = false)
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
            IssueDate = issuedAtUtc.Date;

            if (isPaid)
            {
                SetPaymentDate(issuedAtUtc);
            }
        }

        public void SetPaymentDate(DateTime paymentDate)
        {
            if (!IsIssued)
            {
                throw new InvalidOperationException("Unable to pay invoice that is not issued.");
            }

            if (paymentDate.Date < IssueDate.Value)
            {
                throw new InvalidOperationException("Payment date of an invoice cannot be before it was issued.");
            }

            PaymentDate = paymentDate.Date;
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

        public void SetPrintoutDate(DateTime printoutDate)
        {
            if (!IsIssued)
            {
                throw new InvalidOperationException("Unable to print invoice that is not issued.");
            }

            if (printoutDate.Date < IssueDate.Value)
            {
                throw new InvalidOperationException("Printout date of an invoice cannot be before it was issued.");
            }

            PrintoutDate = printoutDate.Date;
        }

        public void SetTemplateVersion(Guid templateVersionId)
        {
            Ensure.That(templateVersionId, nameof(templateVersionId)).IsNotEmpty();
            TemplateVersionId = templateVersionId;
        }

        public bool IsIssued => IssueDate.HasValue;
        public bool IsPrinted => PrintoutDate.HasValue;
        public bool IsPaid => PaymentDate.HasValue;

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
    }
}