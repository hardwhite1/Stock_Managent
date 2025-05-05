using MyShop.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq.Expressions;

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

        public async Task<ExchangeRates?> GetExchangeRatesAsync(string baseCurrency="USD", int currentPage = 1 ,int pageSize = 10)
        {
         try
            {
                 string url = $"https://api.exchangerate-api.com/v4/latest/{baseCurrency}";

                 HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);

                if(responseMessage.IsSuccessStatusCode)
                {
                    string Json = await responseMessage.Content.ReadAsStringAsync();

                    var exchangeRates =  JsonConvert.DeserializeObject<ExchangeRates>(Json);

                    int totalItems = exchangeRates!.Rates.Count;

                    var paginatedRates = exchangeRates.Rates
                    .Skip((currentPage-1) * pageSize)
                    .Take(pageSize)
                    .ToDictionary(kvp=> kvp.Key, kvp=>kvp.Value);

                    exchangeRates.Rates = paginatedRates;

                    exchangeRates.pagination = new Pagination(
                        totalItems : totalItems,
                        pageSize : pageSize,
                        currentPage : currentPage
                    );

                    return exchangeRates;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<decimal> ConvertCurrencyAsync(decimal amount, string baseCurrency, string targetCurrency)
        {
            try
            {
                var exchangeRates = await GetAllExchangeRateAsync(baseCurrency);

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

        public async Task<ExchangeRates?> GetAllExchangeRateAsync(string baseCurrency="USD")
        {
            string url =$"https://api.exchangerate-api.com/v4/latest/{baseCurrency}";

            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(url);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                string Json = await httpResponseMessage.Content.ReadAsStringAsync();

                var exchangeRates = JsonConvert.DeserializeObject<ExchangeRates?>(Json);

                // var rates = exchangeRates.Rates.ToDictionary(kvp=>kvp.Key, kvp=>kvp.Value);

                return exchangeRates;
            }
            return null;
        }
        
        
    }
}