using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;
using MyShop.Services;

namespace MyShop.Pages.Products
{
    [Authorize]
    public class ProductsListModel : PageModel
    {
        private readonly IAddProducts _addProducts;
        private readonly ApplicationDbContext _applicationDbContext;
        public ProductsListModel(IAddProducts addProducts, ApplicationDbContext applicationDbContext)
        {
            _addProducts = addProducts;
            _applicationDbContext = applicationDbContext;
        }
    
        public IList<ItemsModel> itemsModels { get; set; }
        public Pagination Pagination { get; set; }
        public async Task OnGet(int currentPage = 1, int pageSize = 8)
        {
            var result = await _addProducts.FetchProductsAsync(currentPage, pageSize);
        
            itemsModels = result.itemsModels;
            Pagination = result.Pagination;
        }
        public void  OnPost()
        {
           
            
        }

        public IActionResult OnPostDelete(Guid id)
        {
            // find product by Id
            var products =  _applicationDbContext.itemsModels.FirstOrDefault(p => p.Id == id);

            if(products == null)
            {
                return NotFound();
            }

            // Delete the associated image file
            if(!string.IsNullOrEmpty(products.ImagePath))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", products.ImagePath.TrimStart('/'));

                if(System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            // Remove the product from the database
            _applicationDbContext.itemsModels.Remove(products);
            _applicationDbContext.SaveChanges();

            return RedirectToPage(); 
            
        }
    }
}
