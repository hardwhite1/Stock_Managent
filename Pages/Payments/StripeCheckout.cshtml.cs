using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShop.Services;
using MyShop.Models;
using System.Text.Json;
namespace MyApp.Namespace
{
    public class StripeCheckoutModel : PageModel
    {
        private readonly StripeService _stripeService;

        public StripeCheckoutModel(StripeService stripeService)
        {
            _stripeService = stripeService;
        }

        public List<CartItem>? cartItems { get; set; }

        //Model binding
        [BindProperty]
        public decimal Amount { get; set; }
        public void OnGet()
        {
            LoadCart();
        }

        public async Task<IActionResult> OnPost()
        {
            var successUrl = Url.Page("/Payments/PaymentSuccess", null, null, Request.Scheme);

            var cancelUrl = Url.Page("/Payments/CheckOut", null, null, Request.Scheme);

            var checkOutUrl = await _stripeService.CreateCheckOutSession(Amount, "usd", cancelUrl, successUrl);

            return Redirect(checkOutUrl);
        }

        private void LoadCart()
        {
            var cartJson = HttpContext.Session.GetString("cartItems");

            if (!string.IsNullOrEmpty(cartJson))
            {
                cartItems = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new();
            }
        }

        
    }
}
