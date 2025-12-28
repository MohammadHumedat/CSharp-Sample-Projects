using CarRental.CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CarRental.CarRental.DAL.Data
{
    public class CarRentalDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<RentalContract> RentalContracts { get; set; }

      
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options)
            : base(options)
        {
        }

      
        public CarRentalDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
            if (!optionsBuilder.IsConfigured)
            {
                try
                {
                 
                    var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .Build();

                    var connectionString = configuration.GetSection("constr").Value;

                    
                    if (!string.IsNullOrEmpty(connectionString))
                    {
                        optionsBuilder.UseSqlServer(connectionString);
                    }
                    else
                    {
                        
                        optionsBuilder.UseSqlServer(
                            "Server=(localdb)\\MSSQLLocalDB;Database=CarRentalDB;Trusted_Connection=True;TrustServerCertificate=True;");
                    }
                }
                catch
                {
                   
                    optionsBuilder.UseSqlServer(
                        "Server=(localdb)\\MSSQLLocalDB;Database=CarRentalDB;Trusted_Connection=True;TrustServerCertificate=True;");
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("Role")
                .HasValue<Admin>("Admin")
                .HasValue<RentalAgent>("Agent");

           
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Supplier)
                .WithMany(s => s.Cars)
                .HasForeignKey(c => c.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

         
            modelBuilder.Entity<RentalContract>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.RentalContracts)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RentalContract>()
                .HasOne(r => r.Car)
                .WithMany(c => c.RentalContracts)
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RentalContract>()
                .HasOne(r => r.RentalAgent)
                .WithMany()
                .HasForeignKey(r => r.RentalAgentId)
                .OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<Car>()
                .Property(c => c.PurchaseCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<RentalContract>()
                .Property(r => r.DailyRate)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<RentalContract>()
                .Property(r => r.TotalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<RentalContract>()
                .Property(r => r.ExtraFees)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<RentalContract>()
                .Property(r => r.FinalAmount)
                .HasColumnType("decimal(18,2)");

          
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData( // i can add the admin account from here or using insert into query on user table.
                new Admin
                {
                    UserId = 1,
                    UserName = "admin",
                    Password = "admin123", 
                    IsActive = true
                }
            );

            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    UserId = 3,
                    UserName = "Hmedat",
                    Password = "Hmedat123",
                    IsActive = true
                }
                );
        }
    }
}