using MyShop.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace MyShop.Services
{
    public class ExchangeRatesService
    {
        private readonly HttpClient _httpClient;

        private const string ApiKey = "263ce7ff21223c2b68e96d67";

        public ExchangeRatesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ExchangeRates?> GetExchangeRatesAsync(string baseCurrency="USD")
        {
            string url = $"https://api.exchangerate-api.com/v4/latest/{baseCurrency}";

            HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);

            if(responseMessage.IsSuccessStatusCode)
            {
                string Json = await responseMessage.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ExchangeRates>(Json);
            }

            return null;
        }

        public async Task<decimal> ConvertCurrencyAsync(decimal amount, string baseCurrency, string targetCurrency)
        {
            try
            {
                var exchangeRates = await GetExchangeRatesAsync(baseCurrency);

                if(exchangeRates == null || !exchangeRates.Rates.ContainsKey(targetCurrency))
                {
                    throw new Exception("invalid target currency or unable to fetch exchange rates");
                }
                //Get the exchange rates for the target currency

                decimal rate = exchangeRates.Rates[targetCurrency];

                //Calculate the converted amount
                return amount * rate;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
            
        }
    }
}