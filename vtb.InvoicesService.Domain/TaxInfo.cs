using System;

namespace vtb.InvoicesService.Domain
{
    public class TaxInfo
    {
        public string TaxLabel { get; private set; }
        public decimal TaxMultiplier { get; private set; }

        public TaxInfo(string taxLabel, decimal taxMultiplier)
        {
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TaxInfo)obj);
        }

        protected bool Equals(TaxInfo other)
        {
            return Equals(TaxLabel, other.TaxLabel);
        }

        public override int GetHashCode()
        {
            return TaxLabel.GetHashCode();
        }
    }
}