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
    
        
        
        public List<ItemsModel> itemsModel { get; set; } = new List<ItemsModel>(); // Default to empty list
        public Pagination Pagination { get; set; } = new Pagination(); // Default to empty pagination
        public async Task OnGet(int currentPage = 1, int pageSize = 8)
        {
            try
            {
                var result = await _addProducts.FetchProductsAsync(currentPage, pageSize);
                itemsModel = result.itemsModel ?? new List<ItemsModel>();
                Pagination = result.Pagination ?? new Pagination { CurrentPage = 1, PageSize = pageSize, TotalItems = 0 };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching products: {ex.Message}");
                itemsModel = new List<ItemsModel>();
                Pagination = new Pagination { CurrentPage = 1, PageSize = pageSize, TotalItems = 0 };
            }
        }
        public void  OnPost()
        {
            
        }

        public IActionResult OnPostDelete(Guid id)
        {
            // find product by Id
            var products =  _applicationDbContext.itemsModel.FirstOrDefault(p => p.Id == id);

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
            _applicationDbContext.itemsModel.Remove(products);
            _applicationDbContext.SaveChanges();

            return RedirectToPage(); 
            
        }
        
    }
}
