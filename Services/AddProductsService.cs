using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;

namespace MyShop.Services
{
    public class AddProductsService : IAddProducts
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public AddProductsService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<bool> AddProductsAsync(ItemsModel itemsModel)
        {
            itemsModel.Id = Guid.NewGuid();

            _applicationDbContext.itemsModels.Add(itemsModel);

            var saveResult = await _applicationDbContext.SaveChangesAsync();
            
            return saveResult == 1;

        }
        public async Task<ItemsModel[]> FetchProductsAsync()
        {
           return await _applicationDbContext.itemsModels.ToArrayAsync();
        }

    }
}