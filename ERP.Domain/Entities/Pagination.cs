using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Domain.Entities
{
    public class Pagination
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 7;

        public Pagination(int page, int pageSize)
        {
            Page=page;
            PageSize=pageSize;
        }

    }
}
