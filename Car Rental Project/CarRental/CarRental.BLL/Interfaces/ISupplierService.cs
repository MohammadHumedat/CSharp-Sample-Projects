using CarRental.CarRental.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.BLL.Interfaces
{
    public interface ISupplierService
    {
        IEnumerable<SupplierDto> GetAllSuppliers();
        SupplierDto GetSupplierById(int id);
        void AddSupplier(SupplierDto supplierDto);
        void UpdateSupplier(SupplierDto supplierDto);
        void DeleteSupplier(int supplierId);
        IEnumerable<SupplierDto> SearchSuppliers(string name);
    }
}
