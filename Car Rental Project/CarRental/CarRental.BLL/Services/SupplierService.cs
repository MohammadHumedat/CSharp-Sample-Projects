using CarRental.CarRental.BLL.DTOs;
using CarRental.CarRental.BLL.Interfaces;
using CarRental.CarRental.DAL.Entities;
using CarRental.CarRental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.CarRental.BLL.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public IEnumerable<SupplierDto> GetAllSuppliers()
        {
            var suppliers = _supplierRepository.GetAll();
            return suppliers.Select(s => new SupplierDto
            {
                SupplierId = s.SupplierId,
                SupplierName = s.SupplierName,
                Country = s.Country,
                Contact = s.Contact
            });
        }

        public SupplierDto GetSupplierById(int id)
        {
            var supplier = _supplierRepository.GetById(id);
            if (supplier == null) return null;

            return new SupplierDto
            {
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                Country = supplier.Country,
                Contact = supplier.Contact
            };
        }

        public void AddSupplier(SupplierDto supplierDto)
        {
            var supplier = new Supplier
            {
                SupplierName = supplierDto.SupplierName,
                Country = supplierDto.Country,
                Contact = supplierDto.Contact
            };

            _supplierRepository.Add(supplier);
        }

        public void UpdateSupplier(SupplierDto supplierDto)
        {
            var supplier = _supplierRepository.GetById(supplierDto.SupplierId);
            if (supplier == null)
                throw new Exception("Supplier not found");

            supplier.SupplierName = supplierDto.SupplierName;
            supplier.Country = supplierDto.Country;
            supplier.Contact = supplierDto.Contact;

            _supplierRepository.Update(supplier);
        }

        public void DeleteSupplier(int supplierId)
        {
            var supplier = _supplierRepository.GetById(supplierId);
            if (supplier == null)
                throw new Exception("Supplier not found");

            _supplierRepository.Delete(supplier);
        }

        public IEnumerable<SupplierDto> SearchSuppliers(string name)
        {
            var suppliers = _supplierRepository.Search(name);
            return suppliers.Select(s => new SupplierDto
            {
                SupplierId = s.SupplierId,
                SupplierName = s.SupplierName,
                Country = s.Country,
                Contact = s.Contact
            });
        }
    }
}
