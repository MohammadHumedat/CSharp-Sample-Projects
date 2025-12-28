using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.DAL.Entities
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? Country { get; set; }
        public string? Contact { get; set; }


        public ICollection<Car>? Cars { get; set; }

        public void AddSupplier(Supplier supplier) { }

        public void UpdateSupplier(Supplier supplier) { }

        public void DeleteSupplier(Supplier supplier) { }
    }
}
