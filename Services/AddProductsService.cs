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
        public Pagination Pagination { get; set; }
        public async Task<bool> AddProductsAsync(ItemsModel itemsModel)
        {
            itemsModel.Id = Guid.NewGuid();

            _applicationDbContext.itemsModels.Add(itemsModel);

            var saveResult = await _applicationDbContext.SaveChangesAsync();
            
            return saveResult == 1;

        }
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