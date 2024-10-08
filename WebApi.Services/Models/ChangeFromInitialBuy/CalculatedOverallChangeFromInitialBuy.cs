﻿namespace WebApi.Services.Models.ChangeFromInitialBuy
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class CalculatedOverallChangeFromInitialBuy
    {
        public CalculatedOverallChangeFromInitialBuy()
        {
            CalculatedChangeFromInitialBuyList = [];
        }

        public List<CalculatedChangeFromInitialBuy> CalculatedChangeFromInitialBuyList { get; set; }

        public decimal OverallChangeInPercentage { get; set; }
        public decimal OverallChangeInPriceUsd { get; set; }
    }
}
