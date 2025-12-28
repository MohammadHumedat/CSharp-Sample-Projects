namespace CarRental.CarRental.DAL.Entities
{
    public enum CarStatus
    {
        Available,
        Rented
    }
    public class Car
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public double Mileage { get; set; }
        public decimal PurchaseCost { get; set; }
        public CarStatus Status { get; set; } 

        // forign key for supplier ( convention 4) relationship.
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } // navigator property.
        public ICollection<RentalContract>? RentalContracts { get; set; }  

        public void UpdateCar() { }

        public void DeleteCar()
        {
        }

        public void SearchCar(Car car) { }
    }
}
