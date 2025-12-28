using CarRental.CarRental.BLL.DTOs;
using CarRental.CarRental.BLL.Interfaces;
using CarRental.CarRental.DAL.Entities;
using CarRental.CarRental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq; 

namespace CarRental.CarRental.BLL.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public IEnumerable<CarDto> GetAvailableCars()
        {
            var cars = _carRepository.GetAvailableCars();

            return cars.Select(c => new CarDto
            {
                CarId = c.CarId,
                Brand = c.Brand,
                Model = c.Model,
                Year = c.Year,
                Color = c.Color,
                Mileage = c.Mileage,
                PurchaseCost = c.PurchaseCost,
                Status = c.Status,
                SupplierId = c.SupplierId,
                SupplierName = c.Supplier?.SupplierName
            });
        }

        public IEnumerable<CarDto> GetAllCars()
        {
            var cars = _carRepository.GetAll();

            return cars.Select(c => new CarDto
            {
                CarId = c.CarId,
                Brand = c.Brand,
                Model = c.Model,
                Year = c.Year,
                Color = c.Color,
                Mileage = c.Mileage,
                PurchaseCost = c.PurchaseCost,
                Status = c.Status,
                SupplierId = c.SupplierId,
                SupplierName = c.Supplier?.SupplierName
            });
        }

        public CarDto GetCarById(int id)
        {
            var car = _carRepository.GetById(id);
            if (car == null) return null;

            return new CarDto
            {
                CarId = car.CarId,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Color = car.Color,
                Mileage = car.Mileage,
                PurchaseCost = car.PurchaseCost,
                Status = car.Status,
                SupplierId = car.SupplierId,
                SupplierName = car.Supplier?.SupplierName
            };
        }

        public void AddCar(CarDto carDto)
        {
            var car = new Car
            {
                Brand = carDto.Brand,
                Model = carDto.Model,
                Year = carDto.Year,
                Color = carDto.Color,
                Mileage = carDto.Mileage,
                PurchaseCost = carDto.PurchaseCost,
                Status = CarStatus.Available,
                SupplierId = carDto.SupplierId
            };

            _carRepository.Add(car);
        }

        public void UpdateCar(CarDto carDto)
        {
            var car = _carRepository.GetById(carDto.CarId);
            if (car == null)
                throw new Exception("Car not found");

            car.Brand = carDto.Brand;
            car.Model = carDto.Model;
            car.Year = carDto.Year;
            car.Color = carDto.Color;
            car.Mileage = carDto.Mileage;
            car.PurchaseCost = carDto.PurchaseCost;
            car.SupplierId = carDto.SupplierId;

            _carRepository.Update(car);
        }

        public void DeleteCar(int carId)
        {
            var car = _carRepository.GetById(carId);
            if (car == null)
                throw new Exception("Car not found");

            _carRepository.Delete(car);
        }

        public IEnumerable<CarDto> SearchCars(string brand, string model, int? year)
        {
            var cars = _carRepository.Search(brand, model, year);

            return cars.Select(c => new CarDto
            {
                CarId = c.CarId,
                Brand = c.Brand,
                Model = c.Model,
                Year = c.Year,
                Color = c.Color,
                Mileage = c.Mileage,
                PurchaseCost = c.PurchaseCost,
                Status = c.Status,
                SupplierId = c.SupplierId,
                SupplierName = c.Supplier?.SupplierName
            });
        }
    }
}