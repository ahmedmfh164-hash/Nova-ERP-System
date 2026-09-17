using ERP.Application.Helpers;
using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Mappings;
using ERP.Contacts.Requests.Supplier;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;


namespace ERP.Application.Servicies
{
    public class SupplierService:ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        public SupplierService(ISupplierRepository repo)
        {
            _supplierRepository = repo;
        }


        public async Task<List<SupplierResponseDTO>> GetAllSupplierAsync()
        {
            List<Supplier> Suppliers = await _supplierRepository.GetAllSuppliersAsync();

            return Suppliers.Select(supplier => supplier.ToResponseDTO()).ToList();

        }

        public async Task<int?> AddSupplierAsync(CreatedSupplierDTO dto)
        {
            int? SupplierId =await _supplierRepository.AddSupplierAsync(dto);
        
            return SupplierId;
        }


        


         public async Task<SupplierResponseDTO?> GetSupplierBySupplierIdAsync(int supplierId)
         {
             var supplier = await _supplierRepository.GetSupplierBySupplierIdAsync(supplierId);

             return supplier == null ? null
                 : supplier.ToResponseDTO();
         }

        public async Task<bool> DeleteSupplierAsync(int SupplierId)
        {
            var rowAffected = await _supplierRepository.DeleteSupplierAsync(SupplierId);

            return rowAffected>0;
        }

              public async Task<bool> IsSupplierExistAsync(int supplierId)
              {
                  return await _supplierRepository.isSupplierExistAsync(supplierId);

              }

          


        
    }
}
