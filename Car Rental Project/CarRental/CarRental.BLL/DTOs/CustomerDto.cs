using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.BLL.DTOs
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? LicenseNumber { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
