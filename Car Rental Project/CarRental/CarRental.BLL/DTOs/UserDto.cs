using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.BLL.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; }
    }
}
