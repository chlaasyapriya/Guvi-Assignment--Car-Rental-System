using Assignment__Car_Rental_System.Models;

namespace Assignment__Car_Rental_System.Services
{
    public interface ICarRentalService
    {
        public bool RentCar(int carId, int userId, int noOfDays);
        public bool CheckCarAvailability(int  carId);
        public IEnumerable<Car> GetAvailableCars();
        public void AddCar(Car car);
        public void UpdateCar(int carId,Car car);
        public void DeleteCar(int carId);
    }
}
