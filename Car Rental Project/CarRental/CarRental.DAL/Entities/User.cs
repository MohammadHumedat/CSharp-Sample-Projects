namespace CarRental.CarRental.DAL.Entities
{
    public abstract class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; }


        public bool Login(string username, string password) 
        {
            return UserName == username && Password == password && this.IsActive;

        }
        public void Logout()
        {
            Console.WriteLine($"{UserName} you logged out successfully.");
        }
    }
}


