using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShop.Models;
using MyShop.Services;

namespace MyApp.Namespace
{
    public class CurrencyConvertorModel : PageModel
    {
        private readonly ExchangeRatesService _exchangeRatesService;

         [BindProperty]
        public CurrencyConversion currencyConversion { get; set; }

        public CurrencyConvertorModel(ExchangeRatesService exchangeRatesService)
        {
            _exchangeRatesService =  exchangeRatesService;
        }
       
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                try
                {
                    //Perform the conversion
                    currencyConversion.ConvertedAmount = await _exchangeRatesService.ConvertCurrencyAsync(currencyConversion.Amount, currencyConversion.BaseCurrency, currencyConversion.TargetCurrency);
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return Page();
        }
    }
}
