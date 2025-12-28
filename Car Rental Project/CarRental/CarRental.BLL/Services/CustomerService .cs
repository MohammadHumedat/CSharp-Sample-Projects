using CarRental.CarRental.BLL.DTOs;
using CarRental.CarRental.BLL.Interfaces;
using CarRental.CarRental.DAL.Entities;
using CarRental.CarRental.DAL.Interfaces;

namespace CarRental.CarRental.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<CustomerDto> GetAllCustomers()
        {
            var customers = _customerRepository.GetAll();
            return customers.Select(c => new CustomerDto
            {
                CustomerId = c.CustomerId,
                CustomerName = c.CustomerName,
                LicenseNumber = c.LicenseNumber,
                Phone = c.Phone,
                Address = c.Address
            });
        }

        public CustomerDto GetCustomerById(int id)
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null) return null;

            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                LicenseNumber = customer.LicenseNumber,
                Phone = customer.Phone,
                Address = customer.Address
            };
        }

        public CustomerDto GetCustomerByLicense(string licenseNumber)
        {
            var customer = _customerRepository.GetByLicense(licenseNumber);
            if (customer == null) return null;

            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                LicenseNumber = customer.LicenseNumber,
                Phone = customer.Phone,
                Address = customer.Address
            };
        }

        public void AddCustomer(CustomerDto customerDto)
        {
            var customer = new Customer
            {
                CustomerName = customerDto.CustomerName,
                LicenseNumber = customerDto.LicenseNumber,
                Phone = customerDto.Phone,
                Address = customerDto.Address
            };

            _customerRepository.Add(customer);
        }

        public void UpdateCustomer(CustomerDto customerDto)
        {
            var customer = _customerRepository.GetById(customerDto.CustomerId);
            if (customer == null)
                throw new Exception("Customer not found");

            customer.CustomerName = customerDto.CustomerName;
            customer.LicenseNumber = customerDto.LicenseNumber;
            customer.Phone = customerDto.Phone;
            customer.Address = customerDto.Address;

            _customerRepository.Update(customer);
        }

        public void DeleteCustomer(int customerId)
        {
            var customer = _customerRepository.GetById(customerId);
            if (customer == null)
                throw new Exception("Customer not found");

            _customerRepository.Delete(customer);
        }

        public IEnumerable<CustomerDto> SearchCustomers(string name)
        {
            var customers = _customerRepository.Search(name);
            return customers.Select(c => new CustomerDto
            {
                CustomerId = c.CustomerId,
                CustomerName = c.CustomerName,
                LicenseNumber = c.LicenseNumber,
                Phone = c.Phone,
                Address = c.Address
            });
        }
    }
}
