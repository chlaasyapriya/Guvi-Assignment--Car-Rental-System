using Assignment__Car_Rental_System.Models;

namespace Assignment__Car_Rental_System.Repositories
{
    public interface IUserRepo
    {
        public void AddUser(User user);
        public User GetUserByEmail(string email);
        public User GetUserById(int id);
    }
}
