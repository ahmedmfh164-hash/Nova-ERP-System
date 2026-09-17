using ERP.Contacts.Requests.People;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Contacts.Mappings
{
    public static class CustomerMapping
    {
      
        public static CustomerResponseDTO ToResponseDTO(this Customer customer)
        {
            return new CustomerResponseDTO
            {
                CustomerId=customer.CustomerId,
                PersonId =customer.Person.PersonId,
                PersonName=customer.Person.PersonName,
                Phone=customer.Person.Phone,
                Email=customer.Person.Email,
                Address=customer.Person.Address,
                Gender=customer.Person.Gender,
                ImageGuid=customer.Person.ImageGuid
            };
        }
    }
}
