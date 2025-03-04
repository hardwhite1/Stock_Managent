using MyShop.Models;

namespace MyShop.Services
{
    public interface IAddProducts
    {
        Task<bool> AddProductsAsync(ItemsModel itemsModel);
        Task<(List<ItemsModel> itemsModel, Pagination Pagination)> FetchProductsAsync(int currentPage, int pageSize);

        //  Task <string> UploadFile(IFormFile file);
    }
}