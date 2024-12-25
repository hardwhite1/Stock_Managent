using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShop.Data;
using MyShop.Models;
using MyShop.Services;

namespace MyShop.Pages.Products
{
   // [Authorize]
    public class ProductsListModel : PageModel
    {
        private readonly IAddProducts _addProducts;
        public ProductsListModel(IAddProducts addProducts)
        {
            _addProducts = addProducts;
        }
    
        public IList<ItemsModel> itemsModels { get; set; }
        public async Task OnGet()
        {
            itemsModels = await _addProducts.FetchProductsAsync();
        
         
        }
        public void  OnPost()
        {
           
            
        }
    }
}
