using ERP.Contacts.Requests.People;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Contacts.Mappings
{
    public static class SupplierMapping
    {

      

        public static SupplierResponseDTO ToResponseDTO(this Supplier supplier)
        {
            return new SupplierResponseDTO
            {
                SupplierId=supplier.SupplierId,
               PersonId =supplier.Person.PersonId,
               PersonName=supplier.Person.PersonName,
               Phone=supplier.Person.Phone,
               Email=supplier.Person.Email,
               Address=supplier.Person.Address,
               Gender=supplier.Person.Gender,
               ImageGuid=supplier.Person.ImageGuid
            };
        }
    }
}
