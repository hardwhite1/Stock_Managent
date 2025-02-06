
namespace MyShop.Models
{
    public class CartItemsModel
    {
        public Guid Id { get; set;}

        public ItemsModel itemsModel{ get; set; } = new ItemsModel();

        public int Quantity{ get; set; }
    }
}