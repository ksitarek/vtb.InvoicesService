using EnsureThat;
using System;

namespace vtb.InvoicesService.Domain
{
    public record TaxInfo
    {
        public string TaxLabel { get; }

        public decimal TaxMultiplier { get; }

        public TaxInfo(string taxLabel, decimal taxMultiplier)
        {
            Ensure.That(taxLabel, nameof(taxLabel)).IsNotEmptyOrWhiteSpace();
            Ensure.That(taxMultiplier, nameof(taxMultiplier)).IsGte(0);
            Ensure.That(taxMultiplier, nameof(taxMultiplier)).IsLt(1);

            TaxLabel = taxLabel ?? throw new ArgumentNullException(nameof(taxLabel));
            TaxMultiplier = taxMultiplier;
        }

        public decimal GetNetValue(CalculationDirection calculationDirection, decimal value)
        {
            if (calculationDirection == CalculationDirection.Unknown)
            {
                throw new InvalidOperationException("Calculation direction must be specified");
            }

            return Math.Round(calculationDirection == CalculationDirection.NetToGross
                ? value
                : value / (1 + TaxMultiplier), 2);
        }

        public decimal GetGrossValue(CalculationDirection calculationDirection, decimal value)
        {
            if (calculationDirection == CalculationDirection.Unknown)
            {
                throw new InvalidOperationException("Calculation direction must be specified");
            }

            return Math.Round(calculationDirection == CalculationDirection.GrossToNet
                ? value
                : value * (1 + TaxMultiplier), 2);
        }

        public virtual bool Equals(TaxInfo other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(TaxLabel, other.TaxLabel);
        }

        public override int GetHashCode()
        {
            return TaxLabel.GetHashCode();
        }
    }
}