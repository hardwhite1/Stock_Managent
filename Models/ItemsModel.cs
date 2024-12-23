using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class ItemsModel
    {
        public Guid Id { get; set;}

        [Required]
        [StringLength(6)]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public float UnitsAvailable { get; set; }
    }
}