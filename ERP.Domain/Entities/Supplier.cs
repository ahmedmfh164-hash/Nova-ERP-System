using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Domain.Entities
{
    public class Supplier
    {
        public int SupplierId { get; private set; }
        public Person? Person { get; set; }

        public Supplier(int supplierId,Person? person)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(supplierId);

            SupplierId=supplierId;
            Person=person;
        }


    }
}
