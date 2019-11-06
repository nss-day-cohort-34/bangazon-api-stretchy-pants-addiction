using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BangazonAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int ProductTypeId { get; set; }
        public int CustomerId { get; set; }
    }
}
