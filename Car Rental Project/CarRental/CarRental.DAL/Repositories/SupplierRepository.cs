using CarRental.CarRental.DAL.Data;
using CarRental.CarRental.DAL.Entities;
using CarRental.CarRental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.DAL.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly CarRentalDbContext _context;

        public SupplierRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public Supplier GetById(int id)
        {
            return _context.Suppliers.Find(id);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _context.Suppliers.ToList();
        }

        public IEnumerable<Supplier> Search(string name)
        {
            return _context.Suppliers
                .Where(s => s.SupplierName.Contains(name))
                .ToList();
        }

        public void Add(Supplier entity)
        {
            _context.Suppliers.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Supplier entity)
        {
            _context.Suppliers.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(Supplier entity)
        {
            _context.Suppliers.Remove(entity);
            _context.SaveChanges();
        }

    }
}
