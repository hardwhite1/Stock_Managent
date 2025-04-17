using System.Collections.Generic;   
using MyShop.Services;

namespace MyShop.Models
{
    public class ExchangeRates
    {
        public required string Base { get; set; }

        public required Dictionary<string, decimal> Rates { get; set; }

        public required Pagination pagination {get; set;}

        
    }
}