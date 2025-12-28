using CarRental.CarRental.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CarRental.CarRental.BLL.Interfaces
{
    public interface ICarService
    {
        IEnumerable<CarDto> GetAvailableCars();
        IEnumerable<CarDto> GetAllCars();
        CarDto GetCarById(int id);
        void AddCar(CarDto carDto);
        void UpdateCar(CarDto carDto);
        void DeleteCar(int carId);
        IEnumerable<CarDto> SearchCars(string brand, string model, int? year);
    }
}
