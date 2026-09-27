using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Contacts.Requests.Products
{
    public sealed record UpdateProductDTO
    {
        public int ProductId { get; init; }
        public string ProductName { get; init; }
        public decimal Price { get; init; }
        public decimal Cost { get; init; }
        public int SupplierId { get; init; }
        public int CategoryId { get; init; }
        public int? Quantity { get; init; }
    }
}
