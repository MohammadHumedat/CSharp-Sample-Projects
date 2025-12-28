using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.BLL.DTOs
{
    public class RentCarDto
    {
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public int RentalAgentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DailyRate { get; set; }
    }
}
