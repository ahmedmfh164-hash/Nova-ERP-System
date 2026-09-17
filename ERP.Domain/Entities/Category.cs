using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public Category(int categoryId, string categoryName)
        {
            CategoryId=categoryId;
            CategoryName=categoryName;
        }
    }
}
