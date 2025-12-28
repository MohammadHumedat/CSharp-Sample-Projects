using CarRental.CarRental.BLL.DTOs;
using CarRental.CarRental.BLL.Interfaces;
using CarRental.CarRental.DAL.Entities;
using CarRental.CarRental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.CarRental.BLL.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalContractRepository _rentalContractRepository;
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;

        public RentalService(
            IRentalContractRepository rentalContractRepository,
            ICarRepository carRepository,
            ICustomerRepository customerRepository)
        {
            _rentalContractRepository = rentalContractRepository;
            _carRepository = carRepository;
            _customerRepository = customerRepository;
        }

        public decimal CreateRental(RentCarDto dto)
        {
          
            var customer = _customerRepository.GetById(dto.CustomerId);
            if (customer == null)
                throw new Exception("Customer not found");

           
            var car = _carRepository.GetById(dto.CarId);
            if (car == null)
                throw new Exception("Car not found");

            if (car.Status != CarStatus.Available)
                throw new Exception("Car is not available for rental");

            
            var days = (dto.EndDate - dto.StartDate).Days;
            if (days <= 0)
                throw new Exception("End date must be after start date");

            var totalPrice = days * dto.DailyRate;

    
            var contract = new RentalContract
            {
                CustomerId = dto.CustomerId,
                CarId = dto.CarId,
                RentalAgentId = dto.RentalAgentId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                DailyRate = (double)dto.DailyRate, 
                TotalPrice = (double)totalPrice,    
                ExtraFees = 0,
                FinalAmount = (double)totalPrice    
            };

            _rentalContractRepository.Add(contract);

           
            car.Status = CarStatus.Rented;
            _carRepository.Update(car);

            return totalPrice;
        }

        public void ReturnCar(ReturnCarDto dto)
        {
            var contract = _rentalContractRepository.GetById(dto.ContractId);
            if (contract == null)
                throw new Exception("Rental contract not found");

            if (contract.ActualReturnDate.HasValue)
                throw new Exception("Car has already been returned");

            
            contract.ActualReturnDate = dto.ActualReturnDate;
            contract.ExtraFees = (double)dto.ExtraFees;

           
            contract.CloseContract();

            _rentalContractRepository.Update(contract);

            
            var car = _carRepository.GetById(contract.CarId);
            car.Status = CarStatus.Available;
            _carRepository.Update(car);
        }

        public IEnumerable<RentalContractDto> GetActiveRentals()
        {
            var contracts = _rentalContractRepository.GetActiveRentals();

            return contracts.Select(c => new RentalContractDto
            {
                ContractId = c.ContractId,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                ActualReturnDate = c.ActualReturnDate,
                DailyRate = (decimal)c.DailyRate,
                TotalPrice = (decimal)c.TotalPrice,
                ExtraFees = (decimal)c.ExtraFees,
                FinalAmount = (decimal)c.FinalAmount,
                CustomerId = c.CustomerId,
                CustomerName = c.Customer?.CustomerName,
                CarId = c.CarId,
                CarBrand = c.Car?.Brand,
                CarModel = c.Car?.Model,
                RentalAgentId = c.RentalAgentId,
                RentalAgentName = c.RentalAgent?.UserName
            });
        }

        public IEnumerable<RentalContractDto> GetCustomerRentals(int customerId)
        {
            var contracts = _rentalContractRepository.GetByCustomer(customerId);

            return contracts.Select(c => new RentalContractDto
            {
                ContractId = c.ContractId,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                ActualReturnDate = c.ActualReturnDate,
                DailyRate = (decimal)c.DailyRate,
                TotalPrice = (decimal)c.TotalPrice,
                ExtraFees = (decimal)c.ExtraFees,
                FinalAmount = (decimal)c.FinalAmount,
                CustomerId = c.CustomerId,
                CustomerName = c.Customer?.CustomerName,
                CarId = c.CarId,
                CarBrand = c.Car?.Brand,
                CarModel = c.Car?.Model,
                RentalAgentId = c.RentalAgentId,
                RentalAgentName = c.RentalAgent?.UserName
            });
        }
    }
}