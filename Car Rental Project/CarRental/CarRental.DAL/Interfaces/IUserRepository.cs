
using CarRental.CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.DAL.Interfaces
{
    public interface IUserRepository
    {
        User GetByUsername(string username);
        User GetById(int userId); 
        IEnumerable<User> GetAllAgents();
        void Add(User user);
        void Update(User user);
    }
}
