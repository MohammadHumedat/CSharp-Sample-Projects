using CarRental.CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.BLL.DTOs
{
    public class CarDto
    {
        public int CarId { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public string? Color { get; set; }
        public double Mileage { get; set; }
        public decimal PurchaseCost { get; set; }
        public CarStatus Status { get; set; }

        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
    }
}
