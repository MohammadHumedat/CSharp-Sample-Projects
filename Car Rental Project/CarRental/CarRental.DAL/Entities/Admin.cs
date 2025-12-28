using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.DAL.Entities
{
    public class Admin : User
    {

        List<RentalAgent> RentalAgents  { get; set; } = new List<RentalAgent>();
        List<Supplier> Suppliers { get; set; } = new List<Supplier>();
        List<Car> Cars { get; set; } = new List<Car>();

        public void AddRentalAgent(RentalAgent rentalAgent) { }

        public void UpdateAgent(RentalAgent rentalAgent) { }

        public void ActivateDeactivateAgent(RentalAgent agent, bool status) { }

        public void AddSupplier(Supplier supplier) { }
        public void UpdateSupplier(Supplier supplier) { }
        public void DeleteSupplier(Supplier supplier) { }


        public void AddCar(Car car) { }
        public void UpdateCar(Car car) { }
        public void DeleteCar(Car car) { }

        public void ViewReports() { }
    }

}
