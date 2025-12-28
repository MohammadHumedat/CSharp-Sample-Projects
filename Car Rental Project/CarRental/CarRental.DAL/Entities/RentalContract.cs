using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarRental.CarRental.DAL.Entities
{
    public class RentalContract
    {
        [Key]
        public int ContractId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public double DailyRate { get; set; }
        public double TotalPrice { get; set; }
        public double ExtraFees { get; set; }
        public double FinalAmount { get; set; }


        // relationships [convention 4]
        // forign key for customer
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        // forign key for car
        public int CarId { get; set; }
        public Car Car { get; set; }

        // forign key for agent
        public int RentalAgentId { get; set; }
        public RentalAgent RentalAgent { get; set; }

        public void CalculateTotalPrice()
        {
            TotalPrice = (EndDate - StartDate).Days * DailyRate;
        }


        public void AddExtraFees(double fees)
        {
            ExtraFees += fees;
        }


        public void CloseContract()
        {
            FinalAmount = TotalPrice + ExtraFees;
        }
    }
}
