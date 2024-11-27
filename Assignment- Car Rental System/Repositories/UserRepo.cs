using Assignment__Car_Rental_System.Data;
using Assignment__Car_Rental_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Assignment__Car_Rental_System.Repositories
{
    public class UserRepo: IUserRepo
    {
        private readonly CarRentalDbContext _context;
        public UserRepo(CarRentalDbContext context)
        {
            _context = context;
        }
        public void AddUser(User user)
        {
            _context.users.Add(user);
            _context.SaveChanges();
        }
        public User GetUserByEmail(string email)
        {
            var user = _context.users.FirstOrDefault(x => x.Email == email);
            return user;
        }
        public User GetUserById(int id)
        {
            var user = _context.users.FirstOrDefault(x=>x.Id== id);
            return user;
        }
    }
}
