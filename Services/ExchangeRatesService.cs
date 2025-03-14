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
    }
}