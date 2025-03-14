using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShop.Services;
namespace MyApp.Namespace
{
    public class RatesExchangeModel : PageModel
    {
        private readonly ExchangeRatesService _exchangeRatesService;

        public RatesExchangeModel(ExchangeRatesService exchangeRatesService)
        {
            _exchangeRatesService = exchangeRatesService;
        }
        public async Task<IActionResult> OnGet()
        {
            var exchangeRates = await _exchangeRatesService.GetExchangeRatesAsync();

            return RedirectToPage("RatesExchange", exchangeRates);
        }
    }
}
