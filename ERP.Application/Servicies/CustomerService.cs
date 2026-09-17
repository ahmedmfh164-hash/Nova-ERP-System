using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Mappings;
using ERP.Contacts.Requests.Customer;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Application.Servicies
{
    public class CustomerService:ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository repo)
        {
            _customerRepository = repo;
        }


        public async Task<List<CustomerResponseDTO>> GetAllCustomersAsync()
        {
            List<Customer> Customers = await _customerRepository.GetAllCustomersAsync();

            return Customers.Select(customer=> customer.ToResponseDTO()).ToList();

        }

        public async Task<int?> AddCustomerAsync(CreatedCustomerDTO dto)
        {
            int? CustomerId = await _customerRepository.AddCustomerAsync(dto);

            return CustomerId;
        }


        public async Task<CustomerResponseDTO?> GetCustomerByCustomerIdAsync(int customerId)
        {
            var customer = await _customerRepository.GetCustomerByCustomerIdAsync(customerId);

            return customer == null ? null
                : customer.ToResponseDTO();
        }

        public async Task<bool> DeleteCustomerAsync(int CustomerId)
        {
            var rowAffected = await _customerRepository.DeleteCustomerAsync(CustomerId);

            return rowAffected>0;
        }

        public async Task<bool> IsCustomerExistAsync(int customerId)
        {
            return await _customerRepository.isCustomerExistAsync(customerId);

        }






    }
}
