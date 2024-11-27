using Assignment__Car_Rental_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment__Car_Rental_System.Data
{
    public class CarRentalDbContext: DbContext
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : base(options) { }
        public DbSet<Car> cars {  get; set; }
        public DbSet<User> users { get; set; }
    }
}
