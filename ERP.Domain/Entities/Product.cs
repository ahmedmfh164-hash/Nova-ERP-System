using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public int? Quantity { get; set; }

        public Product(int ProductId, string ProductName, decimal Price, decimal Cost, int SupplierId, int CategoryId, int? Quantity)
        {
            this.ProductId = ProductId;
            this.ProductName = ProductName;
            this.Price = Price;
            this.Cost = Cost;
            this.SupplierId = SupplierId;
            this.CategoryId = CategoryId;
            this.Quantity = Quantity;
        }

    }
}
