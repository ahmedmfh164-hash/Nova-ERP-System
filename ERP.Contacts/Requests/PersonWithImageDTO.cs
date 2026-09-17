using ERP.Contacts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Contacts.Requests
{
    public class PersonWithImageDTO
    {
        public PersonResponseDTO Person { get; set; }
        public FileStream? Image { get; set; }
    }
}
