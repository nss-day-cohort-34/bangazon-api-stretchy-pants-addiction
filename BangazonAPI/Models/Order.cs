using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BangazonAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? PaymentTypeId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool OrderActive { get; set; }

        public List<Product> Products = new List<Product>();

    }

}