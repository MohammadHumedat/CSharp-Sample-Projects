using CarRental.CarRental.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.BLL.Interfaces
{
    public interface IRentalService
    {
        decimal CreateRental(RentCarDto dto);
        void ReturnCar(ReturnCarDto dto);

        IEnumerable<RentalContractDto> GetActiveRentals();
        IEnumerable<RentalContractDto> GetCustomerRentals(int customerId);
    }
}
