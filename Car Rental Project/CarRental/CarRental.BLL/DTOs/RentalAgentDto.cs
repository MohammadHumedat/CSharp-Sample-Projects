using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.BLL.DTOs
{
    public class RentalAgentDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool IsActive { get; set; }
    }
}
