using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;

namespace MyShop.Services
{
    public class AddProductsService : IAddProducts
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment environment1;
        public AddProductsService(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            environment1 = environment;
        }
        public Pagination Pagination { get; set; }
        public async Task<bool> AddProductsAsync(ItemsModel itemsModel)
        {
            itemsModel.Id = Guid.NewGuid();

            _applicationDbContext.itemsModels.Add(itemsModel);

            var saveResult = await _applicationDbContext.SaveChangesAsync();
            
            return saveResult == 1;

        }

        // public async Task<string> UploadFile(IFormFile file)
        //     {
        //         var filePath = Path.Combine(environment1.ContentRootPath, @"wwwroot\Images", file.FileName);

        //         using var fileSream = new FileStream(filePath, FileMode.Create);

        //         await file.CopyToAsync(fileSream);

        //         return filePath;
                
        //     }
        public async Task<(List<ItemsModel> itemsModels, Pagination Pagination)> FetchProductsAsync(int currentPage, int pageSize)
        {
           int totalItems = await _applicationDbContext.itemsModels.CountAsync();

           var pagination = new Pagination(totalItems, currentPage, pageSize);
           
           var products = await _applicationDbContext.itemsModels

            .Skip(pagination.StartIndex)

            .Take(pagination.PageSize)
            
            .ToListAsync();
            return (products, pagination);
        }

    }
}