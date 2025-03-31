using System.Collections.Generic;   

namespace MyShop.Models
{
    public class ExchangeRates
    {
        public required string Base { get; set; }

        public required Dictionary<string, decimal> Rates { get; set; }

        
    }
}