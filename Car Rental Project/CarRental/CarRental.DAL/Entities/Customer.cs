namespace CarRental.CarRental.DAL.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? LicenseNumber { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

        // relationship [one - many]
        public ICollection<RentalContract>? RentalContracts { get; set; }

        public void Register()
        {
            // i will make this logic in repositroy/service.
        }

        public void Update()
        {
            // i will make the logic in repository/service.
        }

        public void ViewHistory()
        {
            // i will make the logic in repository/service.
        }
    }
}
