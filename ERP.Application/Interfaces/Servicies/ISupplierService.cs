using ERP.Contacts.Requests.Supplier;
using ERP.Contacts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Application.Interfaces.Servicies
{
    public interface ISupplierService
    {
        public Task<List<SupplierResponseDTO>> GetAllSupplierAsync();
        public Task<int?> AddSupplierAsync(CreatedSupplierDTO dto);
        public Task<SupplierResponseDTO?> GetSupplierBySupplierIdAsync(int supplierId);
        public Task<bool> DeleteSupplierAsync(int SupplierId);
        public Task<bool> IsSupplierExistAsync(int supplierId);



    }
}
