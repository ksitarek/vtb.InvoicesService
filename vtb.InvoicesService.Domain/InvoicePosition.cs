﻿using System;

namespace vtb.InvoicesService.Domain
{
    public class InvoicePosition
    {
        public string Summary { get; private set; }
        public string Description { get; private set; }
        public string UnitOfMeasure { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal Value { get; private set; }
        public TaxInfo TaxInfo { get; private set; }

        public InvoicePosition(string summary, decimal quantity, TaxInfo taxInfo, decimal value, string unitOfMeasure, string description = "")
        {
            Summary = summary ?? throw new ArgumentNullException(nameof(summary));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Quantity = quantity;
            TaxInfo = taxInfo ?? throw new ArgumentNullException(nameof(taxInfo));
            Value = value;
            UnitOfMeasure = unitOfMeasure ?? throw new ArgumentNullException(nameof(unitOfMeasure));
        }

        public decimal GetNetValue(CalculationDirection calculationDirection)
            => TaxInfo.GetNetValue(calculationDirection, Value);

        public decimal GetGrossValue(CalculationDirection calculationDirection)
            => TaxInfo.GetGrossValue(calculationDirection, Value);

        public decimal GetTotalNetValue(CalculationDirection calculationDirection)
            => TaxInfo.GetNetValue(calculationDirection, Value * Quantity);

        public decimal GetTotalGrossValue(CalculationDirection calculationDirection)
            => TaxInfo.GetGrossValue(calculationDirection, Value * Quantity);

        public decimal GetTaxValue(CalculationDirection calculationDirection)
            => GetGrossValue(calculationDirection) - GetNetValue(calculationDirection);

        public decimal GetTotalTaxValue(CalculationDirection calculationDirection)
            => GetTotalGrossValue(calculationDirection) - GetTotalNetValue(calculationDirection);
    }
}
