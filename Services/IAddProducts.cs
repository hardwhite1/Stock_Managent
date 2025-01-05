using MyShop.Models;

namespace MyShop.Services
{
    public interface IAddProducts
    {
        Task<bool> AddProductsAsync(ItemsModel itemsModel);
        Task<(List<ItemsModel> itemsModels, Pagination Pagination)> FetchProductsAsync(int currentPage, int pageSize);
    }
}