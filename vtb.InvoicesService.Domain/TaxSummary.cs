using System;

namespace vtb.InvoicesService.Domain
{
    public class TaxSummary
    {
        public string TaxLabel { get; private set; }
        public decimal Net { get; private set; }
        public decimal Gross { get; private set; }
        public decimal Tax { get => Gross - Net; }

        public TaxSummary(string taxLabel, decimal net, decimal gross)
        {
            TaxLabel = taxLabel ?? throw new ArgumentNullException(nameof(taxLabel));
            Net = net;
            Gross = gross;
        }
    }
}