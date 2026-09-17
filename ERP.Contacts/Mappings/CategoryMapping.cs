using ERP.Contacts.Requests.Categories;
using ERP.Contacts.Requests.People;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Contacts.Mappings
{
    public static class CategoryMapping
    {
        public static Category ToEntity(this UpdatedCategoryDTO dto)
        {
            return new Category(
                dto.CategoryId,
                dto.CategoryName);
        }

   
        public static CategoryResponseDTO ToResponseDTO(this Category category)
        {
            return new CategoryResponseDTO
            {
              CategoryId=category.CategoryId,
              CategoryName=category.CategoryName
            };
        }

    }
}
