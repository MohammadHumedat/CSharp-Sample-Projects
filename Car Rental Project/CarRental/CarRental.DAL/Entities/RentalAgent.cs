using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.DAL.Entities
{
    public class RentalAgent : User
    {
        // relationships
        public ICollection<Customer> Customers { get; set; } = new List<Customer>();
        public ICollection<RentalContract> RentalContracts { get; set; } = new List<RentalContract>();
        public void RegisterCustomer(Customer customer) { }
        public void UpdateCustomer(Customer customer) { }
        public Customer SearchCustomer(string licenseNumber) { return null; }


        public RentalContract CreateRentalContract(Customer customer, Car car, DateTime startDate, DateTime endDate)
        {
            var contract = new RentalContract
            {
                Customer = customer,
                Car = car,
                RentalAgent = this,
                StartDate = startDate,
                EndDate = endDate
            };
            RentalContracts.Add(contract);
            return contract;
        }

        public void ProcessReturn(RentalContract contract) { }

    }
}
