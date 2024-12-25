using MyShop.Models;

namespace MyShop.Services
{
    public interface IAddProducts
    {
        Task<bool> AddProductsAsync(ItemsModel itemsModel);
        Task<ItemsModel[]> FetchProductsAsync();
    }
}