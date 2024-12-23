using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShop.Data;
using MyShop.Models;
using MyShop.Services;

namespace MyShop.Pages.Products
{
    [Authorize]
    public class ProductsModel : PageModel
    {
        private readonly IAddProducts _addProducts;
        public ProductsModel(IAddProducts addProducts)
        {
            _addProducts = addProducts;
        }
       
        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPostAsync(ItemsModel itemsModel)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToPage("Index");
            }

            var successful = await _addProducts.AddProductsAsync(itemsModel);
            if(!successful)
            {
                return BadRequest("Could not add product!");
            }
            return RedirectToPage("/Products/ProductsList");        
        }
    }
}
