using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Contacts.Requests.Categories
{
    public sealed record UpdatedCategoryDTO
    {
        public int CategoryId {  get; set; }
        public string? CategoryName { get; set; }

    }
}
