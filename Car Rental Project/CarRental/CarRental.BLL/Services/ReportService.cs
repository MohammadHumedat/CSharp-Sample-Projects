using CarRental.CarRental.BLL.Interfaces;
using CarRental.CarRental.DAL.Interfaces;
using CarRental.CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.CarRental.BLL.Services
{
    public class ReportService : IReportService
    {
        private readonly ICarRepository _carRepository;
        private readonly IRentalContractRepository _rentalContractRepository;

        public ReportService(ICarRepository carRepository, IRentalContractRepository rentalContractRepository)
        {
            _carRepository = carRepository;
            _rentalContractRepository = rentalContractRepository;
        }

        public int GetTotalCarsCount()
        {
            return _carRepository.GetAll().Count();
        }

        public int GetAvailableCarsCount()
        {
            return _carRepository.GetAvailableCars().Count();
        }

        public int GetRentedCarsCount()
        {
            return _carRepository.GetAll()
                .Count(c => c.Status == CarStatus.Rented);
        }

        public IEnumerable<string> GetRentedCarsList()
        {
            return _carRepository.GetAll()
                .Where(c => c.Status == CarStatus.Rented)
                .Select(c => $"{c.Brand} {c.Model} ({c.Year})");
        }

        public decimal GetMonthlyIncome(int year, int month)
        {
            try
            {
                var contracts = _rentalContractRepository.GetAll()
                    .Where(c => c.ActualReturnDate.HasValue &&
                               c.ActualReturnDate.Value.Year == year &&
                               c.ActualReturnDate.Value.Month == month)
                    .ToList();

                if (contracts.Count == 0)
                    return 0;

                return (decimal)contracts.Sum(c => c.FinalAmount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating monthly income: {ex.Message}");
                return 0;
            }
        }

        public decimal GetAnnualIncome(int year)
        {
            try
            {
                var contracts = _rentalContractRepository.GetAll()
                    .Where(c => c.ActualReturnDate.HasValue &&
                               c.ActualReturnDate.Value.Year == year)
                    .ToList();

                if (contracts.Count == 0)
                    return 0;

                return (decimal)contracts.Sum(c => c.FinalAmount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating annual income: {ex.Message}");
                return 0;
            }
        }

        public Dictionary<string, int> GetMostRentedCarModels()
        {
            try
            {
                var contracts = _rentalContractRepository.GetAll()
                    .Where(c => c.Car != null)
                    .ToList();

                if (contracts.Count == 0)
                    return new Dictionary<string, int>();

                return contracts
                    .GroupBy(c => $"{c.Car.Brand} {c.Car.Model}")
                    .OrderByDescending(g => g.Count())
                    .Take(10)
                    .ToDictionary(g => g.Key, g => g.Count());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting most rented cars: {ex.Message}");
                return new Dictionary<string, int>();
            }
        }


       

        //  Retrieve how many cars have extra fees if any exists
        public int GetCarsWithExtraFeesCount() // query 1
        {
            try
            {
                return _rentalContractRepository.GetAll()
                    .Where(c => c.ExtraFees > 0)
                    .Select(c => c.CarId)
                    .Distinct()
                    .Count();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 0;
            }

            /*
             SELECT COUNT(DISTINCT CarId) 
             FROM RentalContracts 
             WHERE ExtraFees > 0
             */
        }

        //  For a given customer, retrieve the model of the cars he rented
        public IEnumerable<string> GetCarModelsRentedByCustomer(int customerId) // query 2
        {
            try
            {
                return _rentalContractRepository.GetAll()
                    .Where(c => c.CustomerId == customerId && c.Car != null)
                    .Select(c => $"{c.Car.Brand} {c.Car.Model} ({c.Car.Year})")
                    .Distinct()
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<string>();
            }
        }

        //  delayed in returning the rented cars
        public int GetDelayedCustomersCount()
        {
            try
            {
                return _rentalContractRepository.GetAll()
                    .Where(c => c.ActualReturnDate.HasValue &&
                               c.ActualReturnDate.Value > c.EndDate)
                    .Select(c => c.CustomerId)
                    .Distinct()
                    .Count();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 0;
            }

            /*
             SELECT COUNT(DISTINCT CustomerId)
             FROM RentalContracts
             WHERE ActualReturnDate IS NOT NULL 
             AND ActualReturnDate > EndDate
             */
        }

        //  Retrieve the gold customer (most expensive contract)
        public string GetGoldCustomer()
        {
            try
            {
                var goldContract = _rentalContractRepository.GetAll()
                    .Where(c => c.Customer != null)
                    .OrderByDescending(c => c.FinalAmount)
                    .FirstOrDefault();

                if (goldContract == null)
                    return "No contracts found";

                return $"{goldContract.Customer.CustomerName} - Total: ${goldContract.FinalAmount:N2}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return "Error retrieving gold customer";
            }
        }
    }
}