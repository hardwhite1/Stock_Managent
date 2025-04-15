using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;

namespace MyApp.Namespace
{
    public class CartPageModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CartPageModel(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<ItemsModel> itemsModels{ get; set; } = new();

        public List<CartItem> cartItems{ get; set; } = new();
        public void OnGet()
        {
            LoadCart();
        }

        public IActionResult OnPostAdd(Guid id)
        {
            LoadCart();

            var products =  _applicationDbContext.itemsModel.FirstOrDefault(p => p.Id == id);
            if(products != null)
            {
                var cartItem = cartItems.FirstOrDefault(c => c.itemsModel.Id == id);

                if(cartItem != null)
                {
                    cartItem.Quantity++;
                }
                else
                {
                    cartItems.Add(new CartItem {itemsModel = products, Quantity =1 } );
                }
                SaveCart();
            }
            return RedirectToPage();
            
        }

        public IActionResult OnPostRemove(Guid Id)
        {
            LoadCart();

            cartItems.RemoveAll(c => c.itemsModel.Id == Id);

            SaveCart();

            return RedirectToPage();
        }

        private void LoadCart()
        {
            var cartJson = HttpContext.Session.GetString("cartItems");

            if (!string.IsNullOrEmpty(cartJson))
            {
                cartItems = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new();
            }
        }

        private void SaveCart()
        {
            var cartJson = JsonSerializer.Serialize(cartItems);

            HttpContext.Session.SetString("cartItems", cartJson);
        }
    }
}
