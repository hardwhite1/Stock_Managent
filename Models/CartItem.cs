
namespace MyShop.Models
{
    public class CartItem
    {
       public ItemsModel itemsModel{ get; set; } = new();

        public int Quantity { get; set; }
    }
  
}