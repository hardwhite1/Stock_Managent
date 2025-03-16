
namespace MyShop.Models
{
    public class CurrencyConversion
    {
        public decimal Amount { get; set; }

        public string? BaseCurrency { get; set; }

        public string? TargetCurrency { get; set; }

        public decimal ConvertedAmount { get; set; }
    }
}