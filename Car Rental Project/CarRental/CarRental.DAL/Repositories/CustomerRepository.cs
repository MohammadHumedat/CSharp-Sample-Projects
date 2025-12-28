using CarRental.CarRental.DAL.Data;
using CarRental.CarRental.DAL.Entities;
using CarRental.CarRental.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.CarRental.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CarRentalDbContext _context;

        public CustomerRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public Customer GetByLicense(string licenseNumber)
        {
            return _context.Customers.FirstOrDefault(c => c.LicenseNumber == licenseNumber);
        }

        public Customer GetById(int id)
        {
            return _context.Customers
               .Include(c => c.RentalContracts)
               .FirstOrDefault(c => c.CustomerId == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        
        public IEnumerable<Customer> Search(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return GetAll();

            return _context.Customers
                .Where(c => c.CustomerName.Contains(name))
                .ToList();
        }

        public void Add(Customer entity)
        {
            _context.Customers.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Customer entity)
        {
            _context.Customers.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(Customer entity)
        {
            _context.Customers.Remove(entity);
            _context.SaveChanges();
        }
    }
}