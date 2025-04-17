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
        public Pagination pagination {get; set;}

        public async Task OnGetAsync( int currentPage = 1, int pageSize = 10)
        {
             ExchangeRates = await _exchangeRatesService.GetExchangeRatesAsync("USD", currentPage, pageSize);

        }
    }
}
