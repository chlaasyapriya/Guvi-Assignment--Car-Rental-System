using Assignment__Car_Rental_System.Data;
using Assignment__Car_Rental_System.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Assignment__Car_Rental_System.Repositories
{
    public class CarRepo: ICarRepo
    {
        private readonly CarRentalDbContext _context;
        public CarRepo(CarRentalDbContext context)
        {
            _context = context;
        }
        public void AddCar(Car car)
        {
            _context.cars.Add(car);
            _context.SaveChanges();
        }
        public Car GetCarById(int carId)
        {
            var car= _context.cars.FirstOrDefault(x=>x.Id==carId);
            return car;
        }
        public IEnumerable<Car> GetAvailableCars()
        {
            var cars = _context.cars.Where(x => x.IsAvailable == true).ToList();
            return cars;
        }
        public void UpdateCarAvailability(int carId, bool isAvailable)
        {
            var car=GetCarById(carId);
            car.IsAvailable = isAvailable;
            _context.SaveChanges();
        }
        public void UpdateCarDetails(int carId, Car car)
        {
            var c = GetCarById(carId);
            c.Make=car.Make;
            c.Model=car.Model;
            c.Year=car.Year;
            c.PricePerDay=car.PricePerDay;
            c.IsAvailable=car.IsAvailable;
            _context.SaveChanges();
        }
        public void DeleteCar(int carId)
        {
            var car= GetCarById(carId);
            _context.cars.Remove(car);
            _context.SaveChanges();
        }
    }
}
