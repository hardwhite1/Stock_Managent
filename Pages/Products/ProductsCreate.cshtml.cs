using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShop.Data;
using MyShop.Models;
using MyShop.Services;

namespace MyShop.Pages.Products
{
    [Authorize(Roles = "Admin")]
    public class ProductsCreateModel : PageModel
    {
        private readonly IAddProducts _addProducts;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductsCreateModel(IAddProducts addProducts, IWebHostEnvironment webHostEnvironment)
        {
            _addProducts = addProducts;
            _webHostEnvironment = webHostEnvironment;
        }
        [BindProperty]

        public IFormFile? ImageFile  { get;  set; }

        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPostAsync(ItemsModel itemsModel)
        {
            if(ImageFile != null)
            {
                // Define folder where images will be stored
                var upLoadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

                Directory.CreateDirectory(upLoadsFolder);

                // Create a unique file name for the uploaded image
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);

                // DEfine full path to save the file
                var filePath = Path.Combine(upLoadsFolder, uniqueFileName);

                //save file to the server
                using(var fileSream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileSream);
                }
                // set the ImagePath property to the relative path of the uploaded image
                itemsModel.ImagePath = $"/Images/{uniqueFileName}";
            }

          
            if(!ModelState.IsValid)
            {
                foreach(var error in ModelState.Values.SelectMany(p => p.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
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
