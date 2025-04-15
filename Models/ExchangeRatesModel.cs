using System.Collections.Generic;   
using MyShop.Services;

namespace MyShop.Models
{
    public class ExchangeRates
    {
        public string? Base { get; set; }

        public Dictionary<string, decimal>? Rates { get; set; }

        public Pagination pagination {get; set;}

        
    }
}