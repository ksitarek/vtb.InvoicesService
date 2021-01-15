using Automatonymous;
using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;

namespace vtb.InvoicesService.Domain
{
    public class Invoice : SagaStateMachineInstance
    {
        public Guid InvoiceId { get; set; }

        public string State { get; set; }

        public Guid CorrelationId
        {
            get => InvoiceId;
            set => InvoiceId = value;
        }

        public Guid TemplateVersionId { get; set; }
        public DateTime DraftCreatedAtUtc { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PrintoutDate { get; set; }
        public InvoiceNumber InvoiceNumber { get; set; }
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public Guid IssuerId { get; set; }
        public Currency Currency { get; set; }
        public CalculationDirection CalculationDirection { get; set; }
        public virtual List<InvoicePosition> InvoicePositions { get; set; } = new();

        public Invoice(DateTime draftCreatedAtUtc, Guid templateVersionId, Guid buyerId, Guid sellerId,
            Currency currency, CalculationDirection calculationDirection)
        {
            Ensure.That(draftCreatedAtUtc, nameof(draftCreatedAtUtc)).IsLte(DateTime.UtcNow);
            Ensure.That(templateVersionId, nameof(templateVersionId)).IsNotEmpty();
            Ensure.That(buyerId, nameof(buyerId)).IsNotEmpty();
            Ensure.That(sellerId, nameof(sellerId)).IsNotEmpty();

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
            Currency = currency;
            CalculationDirection = calculationDirection;
        }

        public void Issue(InvoiceNumber invoiceNumber, DateTime issuedAtUtc, Guid issuerId)
        {
            Ensure.That(issuerId, nameof(issuerId)).IsNotEmpty();

            if (IssueDate != null)
            {
                throw new InvalidOperationException("Cannot issue invoice that already has been issued.");
            }

            if (!InvoicePositions.Any())
            {
                throw new InvalidOperationException("Unable to issue invoice without any positions");
            }

            InvoiceNumber = invoiceNumber;
            IssueDate = issuedAtUtc.Date;
            IssuerId = issuerId;
        }

        public void SetPaymentDate(DateTime paymentDate)
        {
            if (IssueDate == null || paymentDate.Date < IssueDate.Value)
            {
                throw new InvalidOperationException("Payment date of an invoice cannot be before it was issued.");
            }

            PaymentDate = paymentDate.Date;
        }

        public void SetPositions(List<InvoicePosition> positions)
        {
            if (positions.Any())
            {
                ValidateOrdinalNumbers(positions.Select(x => x.OrdinalNumber));
            }

            InvoicePositions.Clear();
            InvoicePositions.AddRange(positions);
        }

        public void SetPrintoutDate(DateTime printoutDate)
        {
            if (IssueDate == null || printoutDate.Date < IssueDate.Value)
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