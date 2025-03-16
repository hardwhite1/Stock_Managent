using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShop.Models;
using MyShop.Services;
namespace MyShop.Pages.Rates
{
    public class RatesExchangeModel : PageModel
    {
        private readonly ExchangeRatesService _exchangeRatesService;

        public ExchangeRates? ExchangeRates { get; private set; }

        public RatesExchangeModel(ExchangeRatesService exchangeRatesService)
        {
            _exchangeRatesService = exchangeRatesService;
        }

        public async Task OnGetAsync()
        {
             ExchangeRates = await _exchangeRatesService.GetExchangeRatesAsync();

        }
    }
}
