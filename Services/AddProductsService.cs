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
            if(itemsModel == null || string.IsNullOrWhiteSpace(itemsModel.Name))
                return false;

            //Check if an item with the same name already exists
            var existingItem = await _applicationDbContext.itemsModel.FirstOrDefaultAsync(i => i.Name == itemsModel.Name);
            
            if(existingItem != null)
            {
                //product exists, update its UnitsAvailable property
                existingItem.UnitsAvailable += itemsModel.UnitsAvailable;

                _applicationDbContext.itemsModel.Update(existingItem);
            }
            else
            {
                //New product, assign new ID

                itemsModel.Id = Guid.NewGuid();

                await _applicationDbContext.itemsModel.AddAsync(itemsModel);
            }

            var saveResult = await _applicationDbContext.SaveChangesAsync();

            return saveResult > 0;

        }

        // public async Task<string> UploadFile(IFormFile file)
        //     {
        //         var filePath = Path.Combine(environment1.ContentRootPath, @"wwwroot\Images", file.FileName);

        //         using var fileSream = new FileStream(filePath, FileMode.Create);

        //         await file.CopyToAsync(fileSream);

        //         return filePath;
                
        //     }
        public async Task<(List<ItemsModel> itemsModel, Pagination Pagination)> FetchProductsAsync(int currentPage, int pageSize)
        {
           int totalItems = await _applicationDbContext.itemsModel.CountAsync();

           var pagination = new Pagination(totalItems, currentPage, pageSize);
           
           var products = await _applicationDbContext.itemsModel

            .Skip(pagination.StartIndex)

            .Take(pagination.PageSize)
            
            .ToListAsync();
            return (products, pagination);
        }

    }
}