using CarRental.CarRental.DAL.Data;
using CarRental.CarRental.DAL.Entities;
using CarRental.CarRental.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.CarRental.DAL.Repositories
{
    public class RentalContractRepository : IRentalContractRepository
    {
        private readonly CarRentalDbContext _context;

        public RentalContractRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public RentalContract GetById(int id)
        {
            return _context.RentalContracts.Include(c => c.Customer).Include(c => c.Car).Include(c => c.RentalAgent).FirstOrDefault(c => c.ContractId == id);
        }
        public IEnumerable<RentalContract> GetAll()
        {
            //return _context.RentalContracts
            //    .Include(r => r.Customer)
            //    .Include(r => r.Car)
            //    .Include(r => r.RentalAgent)
            //    .ToList();
            try
            {
                return _context.RentalContracts
                    .Include(r => r.Customer)
                    .Include(r => r.Car)
                    .ThenInclude(c => c.Supplier)
                    .Include(r => r.RentalAgent)
                    .ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetAll: {ex.Message}");
                return new List<RentalContract>();
            }
        }
        public IEnumerable<RentalContract> GetActiveRentals()
        {
            return _context.RentalContracts
                .Include(r => r.Customer)
                .Include(r => r.Car)
                .Include(r => r.RentalAgent)
                .Where(r => r.ActualReturnDate == null)
                .ToList();
        }

        public IEnumerable<RentalContract> GetByCustomer(int customerId)
        {
            return _context.RentalContracts
                .Include(r => r.Customer)
                .Include(r => r.Car)
                .Include(r => r.RentalAgent)
                .Where(r => r.CustomerId == customerId)
                .ToList();
        }

        public void Add(RentalContract entity)
        {
            _context.RentalContracts.Add(entity);
            _context.SaveChanges();
        }

        public void Update(RentalContract entity)
        {
            _context.RentalContracts.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(RentalContract entity)
        {
            _context.RentalContracts.Remove(entity);
            _context.SaveChanges();
        }
    }
}
