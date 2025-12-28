using CarRental.CarRental.DAL.Data;
using CarRental.CarRental.DAL.Entities;
using CarRental.CarRental.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.CarRental.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CarRentalDbContext _context;

        public UserRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(c => c.UserName == username);
        }

       
        public User GetById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public IEnumerable<User> GetAllAgents()
        {
            return _context.Users.Where(u => u.Role == "Agent").ToList();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}