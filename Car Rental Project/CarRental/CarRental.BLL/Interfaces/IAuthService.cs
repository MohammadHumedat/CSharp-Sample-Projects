using CarRental.CarRental.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.BLL.Interfaces
{
    public interface IAuthService
    {
        UserDto Login(string username, string password);
    }
}
