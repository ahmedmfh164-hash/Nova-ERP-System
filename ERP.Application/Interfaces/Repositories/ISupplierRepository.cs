using ERP.Contacts.Requests.Supplier;
using ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Application.Interfaces.Repositories
{
    public interface ISupplierRepository
    {
        public Task<List<Supplier>> GetAllSuppliersAsync();
        public Task<int> AddSupplierAsync(CreatedSupplierDTO supplierDTO);
        public Task<int> DeleteSupplierAsync(int supplierId);
        public Task<Supplier> GetSupplierBySupplierIdAsync(int supplierId);
        public Task<bool> isSupplierExistAsync(int supplierId);


    }
}
