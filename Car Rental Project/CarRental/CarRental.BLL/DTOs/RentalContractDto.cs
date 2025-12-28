using System;

namespace CarRental.CarRental.BLL.DTOs
{
    public class RentalContractDto
    {
        public int ContractId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public decimal DailyRate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal ExtraFees { get; set; }
        public decimal FinalAmount { get; set; }

        public int CustomerId { get; set; }
        public string? CustomerName { get; set; } 

        public int CarId { get; set; }
        public string? CarBrand { get; set; } 
        public string? CarModel { get; set; } 

        public int RentalAgentId { get; set; }
        public string? RentalAgentName { get; set; } 
    }
}