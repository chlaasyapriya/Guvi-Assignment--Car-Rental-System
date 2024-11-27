using Assignment__Car_Rental_System.Models;

namespace Assignment__Car_Rental_System.Repositories
{
    public interface ICarRepo
    {
        public void AddCar(Car car);
        public Car GetCarById(int carId);
        public IEnumerable<Car> GetAvailableCars();
        public void UpdateCarAvailability(int  carId,bool isAvailable);
        public void UpdateCarDetails(int carId, Car car);
        public void DeleteCar(int carId);
    }
}
