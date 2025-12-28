using CarRental.CarRental.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.BLL.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDto> GetAllCustomers();
        CustomerDto GetCustomerById(int id);
        CustomerDto GetCustomerByLicense(string licenseNumber);
        void AddCustomer(CustomerDto customerDto);
        void UpdateCustomer(CustomerDto customerDto);
        void DeleteCustomer(int customerId);
        IEnumerable<CustomerDto> SearchCustomers(string name);
    }
}
