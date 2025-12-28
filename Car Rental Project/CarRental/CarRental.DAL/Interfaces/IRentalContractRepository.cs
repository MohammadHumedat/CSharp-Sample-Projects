using CarRental.CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.DAL.Interfaces
{
    public interface IRentalContractRepository : IRepository<RentalContract>
    {
        RentalContract GetById(int id);
        IEnumerable<RentalContract> GetAll();
        IEnumerable<RentalContract> GetActiveRentals();
        IEnumerable<RentalContract> GetByCustomer(int customerId);
        void Add(RentalContract entity);
        void Update(RentalContract entity);
        void Delete(RentalContract entity);

    }
}
