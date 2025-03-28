using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShop.Services;
namespace MyApp.Namespace
{
    public class StripeCheckoutModel : PageModel
    {
        private readonly StripeService _stripeService;

        public StripeCheckoutModel(StripeService stripeService)
        {
            _stripeService = stripeService;
        }

        //Model binding
        [BindProperty]
        public decimal Amount { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var successUrl = Url.Page("/PaymentSuccess", null, null, Request.Scheme);

            var cancelUrl = Url.Page("/CheckOut", null, null, Request.Scheme);

            var checkOutUrl = await _stripeService.CreateCheckOutSession(Amount, "usd", cancelUrl, successUrl);

            return Redirect(checkOutUrl);
        }

        
    }
}
