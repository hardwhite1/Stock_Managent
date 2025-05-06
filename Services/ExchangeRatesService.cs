using MyShop.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Linq.Expressions;

namespace MyShop.Services
{
    public class ExchangeRatesService
    {
        private readonly HttpClient _httpClient;

        private readonly ILogger<ExchangeRatesService> _lloger;

        private const string ApiKey = "263ce7ff21223c2b68e96d67";

        public ExchangeRatesService(HttpClient httpClient, ILogger<ExchangeRatesService> lloger)
        //Constructor to initialize the HttpClient and ILogger instances
        //This constructor is used to inject the HttpClient and ILogger dependencies into the ExchangeRatesService class
            
        {
            _httpClient = httpClient;

            _lloger = lloger;
        }

        public async Task<ExchangeRates?> GetExchangeRatesAsync(string baseCurrency = "USD", int currentPage = 1, int pageSize = 10)
        {
            try
            {
                string url = $"https://api.exchangerate-api.com/v4/latest/{baseCurrency}";

                HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);

                if (responseMessage.IsSuccessStatusCode)
                {
                    string Json = await responseMessage.Content.ReadAsStringAsync();

                    var exchangeRates = JsonConvert.DeserializeObject<ExchangeRates>(Json);

                    int totalItems = exchangeRates!.Rates.Count;

                    var paginatedRates = exchangeRates.Rates
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                    exchangeRates.Rates = paginatedRates;

                    exchangeRates.pagination = new Pagination(
                        totalItems: totalItems,
                        pageSize: pageSize,
                        currentPage: currentPage
                    );

                    return exchangeRates;
                }


            }
            catch (Exception ex)
            {
                _lloger.LogError(ex, "Error fetching exchange rates from API: {Message}", ex.Message);
            }

            return null;
        }

        public async Task<decimal> ConvertCurrencyAsync(decimal amount, string baseCurrency, string targetCurrency)
        {
            try
            {
                var exchangeRates = await GetAllExchangeRateAsync(baseCurrency);

                if (exchangeRates == null || !exchangeRates.Rates.ContainsKey(targetCurrency))
                {
                    throw new ArgumentException($"invalid target currency or unable to fetch exchange rates");
                }
                //Get the exchange rates for the target currency

                decimal rate = exchangeRates.Rates[targetCurrency];

                //Calculate the converted amount
                return amount * rate;
            }
            catch (Exception ex)
            {
                _lloger.LogError(ex, "Error converting currency: {Message}", ex.Message);

                throw;
            }

        }

        public async Task<ExchangeRates?> GetAllExchangeRateAsync(string baseCurrency = "USD")
        {
            try
            {
                string url = $"https://api.exchangerate-api.com/v4/latest/{baseCurrency}";

                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string Json = await httpResponseMessage.Content.ReadAsStringAsync();

                    var exchangeRates = Newtonsoft.Json.JsonConvert.DeserializeObject<ExchangeRates?>(Json);

                    //var rates = exchangeRates.Rates.ToDictionary(kvp=>kvp.Key, kvp=>kvp.Value);

                    return exchangeRates;
                }
            }
            catch (Exception ex)
            {
                _lloger.LogError(ex, "Error fetching exchange rates from API: {Message}", ex.Message);
            }

            return null;
        }


    }
}