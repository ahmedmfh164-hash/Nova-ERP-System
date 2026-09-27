using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Contacts.Requests.Pagination
{
    public sealed record PaginationRequestDTO
    {
        public int Page { get; set; }
        public int PageSize { get; set; }


    }
}
