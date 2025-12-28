using CarRental.CarRental.DAL.Data;
using CarRental.CarRental.DAL.Entities;
using CarRental.CarRental.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.DAL.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentalDbContext _context;

        public CarRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public Car GetById(int id)
        {
            return _context.Cars.Include(c => c.Supplier).FirstOrDefault(c => c.CarId == id);
        }

        public IEnumerable<Car> GetAll()
        {
            return  _context.Cars
                .Include(c => c.Supplier)
                .ToList();
        }

        public IEnumerable<Car> GetAvailableCars()
        {
            return _context.Cars.Include(c => c.Supplier).Where(c => c.Status == CarStatus.Available).ToList();
        }
        public IEnumerable<Car> Search(string brand, string model, int? year)
        {
            var query = _context.Cars.Include(c => c.Supplier).AsQueryable();

            if (!string.IsNullOrEmpty(brand))
                query = query.Where(c => c.Brand.Contains(brand));

            if (!string.IsNullOrEmpty(model))
                query = query.Where(c => c.Model.Contains(model));

            if (year.HasValue)
                query = query.Where(c => c.Year == year.Value);

            return query.ToList();
        }
        public void Add(Car entity)
        {
            _context.Cars.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Car entity)
        {
            _context.Cars.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(Car entity)
        {
            _context.Cars.Remove(entity);
            _context.SaveChanges();
        }
    }
}
