using ERP.Contacts.Requests.Customer;
using ERP.Contacts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Application.Interfaces.Servicies
{
    public interface ICustomerService
    {
        public Task<List<CustomerResponseDTO>> GetAllCustomersAsync();
        public Task<int?> AddCustomerAsync(CreatedCustomerDTO dto);
        public Task<CustomerResponseDTO?> GetCustomerByCustomerIdAsync(int customerId);
        public Task<bool> DeleteCustomerAsync(int CustomerId);
        public Task<bool> IsCustomerExistAsync(int customerId);


    }
}
