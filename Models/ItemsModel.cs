
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class ItemsModel
    {
        public Guid Id { get; set;}

        [Required]
        [StringLength(6)]
        public string? Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price Must be grater than zero")]
        public float Price { get; set; }

        [Required]
        [StringLength(100)]
        public string? Description { get; set; }

        public float UnitsAvailable { get; set; }

        [Required]
        public string? ImagePath { get; set; }

        // public static implicit operator LinkedList<object>(ItemsModel v)
        // {
        //     throw new NotImplementedException();
        // }
    }
}