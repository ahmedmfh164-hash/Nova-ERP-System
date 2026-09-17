using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; private set; }
        public Person? Person { get; set; }

        public Customer(int customerId, Person? person)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(customerId);

            CustomerId = customerId;
            Person=person;
        }

    }
}
