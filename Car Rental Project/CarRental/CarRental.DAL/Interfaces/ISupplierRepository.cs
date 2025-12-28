using CarRental.CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.DAL.Interfaces
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        IEnumerable<Supplier> Search(string name);
    }
}
