using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.BLL.DTOs
{
    public class SupplierDto
    {
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? Country { get; set; }
        public string? Contact { get; set; }
    }
}
