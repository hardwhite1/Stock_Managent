using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShop.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using MyShop.Data;

namespace MyShop.Pages.Products
{
    public class CartsModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CartsModel(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<CartItemsModel> Cart{ get; set; } = new();

        public void OnGet()
        {
            LoadCart();
        }

        public IActionResult OnPostAdd(Guid productId)
        {
            LoadCart();

            // if(!ModelState.IsValid)
            // {
            //     return Page();
            // }

            var product = _applicationDbContext.itemsModels.FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                var cartItems = Cart.FirstOrDefault(c => c.itemsModel.Id == productId);

                if(cartItems != null) 
                {
                    cartItems.Quantity++;
                }
                else
                {
                     Cart.Add(new CartItemsModel{ itemsModel = product, Quantity = 1});
                }
                SaveCart();
            }
            return RedirectToPage();
        }

        public IActionResult OnPostRemove(Guid productId)
        {
            LoadCart();
            Cart.RemoveAll(c =>c.itemsModel.Id == productId);
            SaveCart();
            return RedirectToPage();
        }

        

        private void LoadCart()
        {

            var cartJson = HttpContext.Session.GetString("Cart");
            if(!string.IsNullOrEmpty(cartJson))
            {
                Cart = JsonSerializer.Deserialize<List<CartItemsModel>>(cartJson) ?? new();
            }

        }

        private void SaveCart()
        {
            var cartJson = JsonSerializer.Serialize(Cart);
            HttpContext.Session.SetString("Cart", cartJson);
        }
    }
}