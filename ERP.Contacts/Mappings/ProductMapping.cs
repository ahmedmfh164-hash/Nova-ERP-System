using ERP.Contacts.Requests.Products;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Contacts.Mappings
{
    public static class ProductMapping
    {
        public static Product ToEntity(this CreateProductDTO dto)
        {
            return new Product(
                0,
                dto.ProductName,
                Convert.ToDecimal(dto.Price),
                Convert.ToDecimal(dto.Cost),
                dto.SupplierId,
                dto.CategoryId,
                Convert.ToInt32(dto.Quantity));
        }

        public static Product ToEntity(this UpdateProductDTO dto)
        {
            return new Product(
                dto.ProductId,
              dto.ProductName,
                Convert.ToDecimal(dto.Price),
                Convert.ToDecimal(dto.Cost),
                dto.SupplierId,
                dto.CategoryId,
                Convert.ToInt32(dto.Quantity));
        }

        public static ProductResponseDTO ToResponseDTO(this Product product)
        {
            return new ProductResponseDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                Cost = product.Cost,
                Price = product.Price,
                Quantity = product.Quantity
            };
        }
    }
}   

