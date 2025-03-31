
namespace MyShop.Models
{
    public class CurrencyConversion
    {
        public decimal Amount { get; set; }

        public required string BaseCurrency { get; set; }

        public required string TargetCurrency { get; set; }

        public decimal ConvertedAmount { get; set; }
    }
}