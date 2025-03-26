using Stripe;
using Stripe.Checkout;
using Microsoft.Extensions.Configuration;   
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShop.Services
{
    public class StripeService
    {
        private readonly IConfiguration _configuration;

        public StripeService (IConfiguration configuration)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        public async Task<String> CreateCheckOutSession(decimal amount, string currency, string cancelUrl, string successUrl)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>{ "card" },

                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = currency,
                            UnitAmount = (long)(amount * 100),
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Purchase from Hardwhite Enterprise"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,  
            };
            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            return session.Url;
        }
    }
}