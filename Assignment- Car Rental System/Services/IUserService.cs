using Assignment__Car_Rental_System.Models;

namespace Assignment__Car_Rental_System.Services
{
    public interface IUserService
    {
        public bool RegisterUser(User user);
        public string AuthenticateUser(string email, string password);
    }
}
