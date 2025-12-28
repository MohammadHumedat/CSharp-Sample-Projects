using CarRental.CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.DAL.Interfaces
{
   public interface ICarRepository : IRepository<Car>
    {
        IEnumerable<Car> GetAll();
        IEnumerable<Car> GetAvailableCars();
        IEnumerable<Car> Search(string brand, string model, int? year);
        Car GetById(int Id);
        void Add(Car entity);
        void Update(Car entity);
        void Delete(Car entity);

    }
}
