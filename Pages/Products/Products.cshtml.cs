using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShop.Data;
using MyShop.Models;
using MyShop.Services;

namespace MyShop.Pages.Products
{
    [Authorize(Roles = "Admin")]
    public class ProductsModel : PageModel
    {
        private readonly IAddProducts _addProducts;
        public ProductsModel(IAddProducts addProducts)
        {
            _addProducts = addProducts;
        }

        public string FilePath { get;  set; }

        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPostAsync(ItemsModel itemsModel, IFormFile file)
        {

            if(file != null)
            {
                           
             FilePath = await _addProducts.UploadFile(file);
            }

            if(!ModelState.IsValid)
            {
                return RedirectToPage("/Index");
            }

            var successful = await _addProducts.AddProductsAsync(itemsModel);

            if(!successful)
            {
                return BadRequest("Could not add product!");
            }
            return RedirectToPage("/Products/ProductsList");   // POST-REDIRECT-GET     
        }
    }
}
