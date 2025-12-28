using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.BLL.Interfaces
{
    public interface IReportService
    {
        int GetTotalCarsCount();
        int GetAvailableCarsCount();
        int GetRentedCarsCount();
        IEnumerable<string> GetRentedCarsList();
        decimal GetMonthlyIncome(int year, int month);
        decimal GetAnnualIncome(int year);
        Dictionary<string, int> GetMostRentedCarModels();

        int GetCarsWithExtraFeesCount();
        IEnumerable<string> GetCarModelsRentedByCustomer(int customerId);
        int GetDelayedCustomersCount();
        string GetGoldCustomer();
    }
}
