using ERP.Contacts.Requests.Customer;
using ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Application.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        public Task<List<Customer>> GetAllCustomersAsync();
        public Task<int> AddCustomerAsync(CreatedCustomerDTO customerDTO);
        public Task<int> DeleteCustomerAsync(int customerId);
        public Task<Customer> GetCustomerByCustomerIdAsync(int customerId);
        public Task<bool> isCustomerExistAsync(int customerId);



    }
}
