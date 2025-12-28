using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.BLL.DTOs
{
    public class ReturnCarDto
    {
        public int ContractId { get; set; }
        public DateTime ActualReturnDate { get; set; }
        public decimal ExtraFees { get; set; }
    }
}
