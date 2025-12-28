using CarRental.CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.DAL.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByLicense(string licenseNumber);
        Customer GetById(int id);
        IEnumerable<Customer> GetAll();
        IEnumerable<Customer> Search(string name);
        void Add(Customer entity);
        void Update(Customer entity);
        void Delete(Customer entity);
    }
}
