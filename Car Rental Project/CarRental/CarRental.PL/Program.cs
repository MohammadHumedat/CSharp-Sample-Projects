using CarRental.CarRental.BLL.Interfaces;
using CarRental.CarRental.BLL.Services;
using CarRental.CarRental.DAL.Data;
using CarRental.CarRental.DAL.Interfaces;
using CarRental.CarRental.DAL.Repositories;
using CarRental.CarRental.PL.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.CarRental.PL
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

           
            var services = new ServiceCollection();
            ConfigureServices(services, configuration);
            ServiceProvider = services.BuildServiceProvider();

            
            Application.Run(ServiceProvider.GetRequiredService<LoginForm>());
        }
        private static void ConfigureServices(ServiceCollection services, IConfiguration configuration)
        {

            
            
            services.AddDbContext<CarRentalDbContext>(options =>
                options.UseSqlServer(configuration.GetSection("constr").Value));

            // to enhance the dependuncy injection
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IRentalContractRepository, RentalContractRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();

           
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IRentalService, RentalService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IRentalAgentService, RentalAgentService>();

          
            services.AddTransient<LoginForm>();
            services.AddTransient<AdminDashboardForm>();
            services.AddTransient<RentalAgentDashboardForm>();
            services.AddTransient<ManageCarsForm>();
            services.AddTransient<ManageCustomersForm>();
            services.AddTransient<ManageSuppliersForm>();
            services.AddTransient<ManageRentalAgentsForm>();
            services.AddTransient<CreateRentalForm>();
            services.AddTransient<ReturnCarForm>();
            services.AddTransient<ReportsForm>();
        }
    }
}



/* note this is the workflow of data
 
RentalForm (PL)
     ? Calls
RentalService (BLL)
     ? Calls
RentalRepository (DAL)
     ?
EF Core ? Database
 */

// form call service call repository call DBcontext
// the main logic in services, Business rules, Validation, Transactions.

/*
 User clicks button
?
Form (PL)

CarService (BLL)

CarRepository (DAL)

DbContext

Database
 */