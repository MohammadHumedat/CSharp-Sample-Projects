using CarRental.CarRental.BLL.DTOs;
using CarRental.CarRental.BLL.Interfaces;
using CarRental.CarRental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDto Login(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);

            if (user == null)
                throw new Exception("User not found");

            if (!user.IsActive)
                throw new Exception("User account is deactivated");

            if (user.Login(username, password))
            {
                return new UserDto
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Role = user.Role,
                    IsActive = user.IsActive
                };
            }

            throw new Exception("Invalid username or password");
        }
    }
}
