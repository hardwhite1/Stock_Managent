using System.Collections.Generic;   

namespace MyShop.Models
{
    public class ExchangeRates
    {
        public string? Base { get; set; }

        public Dictionary<string, decimal>? Rates { get; set; }

        
    }
}