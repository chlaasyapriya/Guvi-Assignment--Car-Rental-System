using Assignment__Car_Rental_System.Models;
using Assignment__Car_Rental_System.Repositories;

namespace Assignment__Car_Rental_System.Services
{
    public class CarRentalService: ICarRentalService
    {
        private readonly ICarRepo _carRepo;
        private readonly NotificationService _notificationService;
        private readonly IUserRepo _userRepo;
        public CarRentalService(ICarRepo carRepo,NotificationService notificationService,IUserRepo userRepo)
        {
            _carRepo = carRepo;
            _notificationService = notificationService;
            _userRepo = userRepo;
        }
        public bool RentCar(int carId, int userId, int noOfDays)
        {
            var car=_carRepo.GetCarById(carId);
            if(car ==null || !car.IsAvailable)
                   return false;
            car.IsAvailable = false;
            _carRepo.UpdateCarAvailability(carId, false);
            var user=_userRepo.GetUserById(userId);
            _notificationService.SendNotification(user.Email, user.Name, car.Model, car.Make, noOfDays);
            return true;
            
        }
        public bool CheckCarAvailability(int carId)
        {
            return _carRepo.GetCarById(carId).IsAvailable? true: false;
        }
 
        public IEnumerable<Car> GetAvailableCars()
        {
            return _carRepo.GetAvailableCars();
        }
        public void AddCar(Car car)
        {
            _carRepo.AddCar(car);
        }
        public void UpdateCar(int carId, Car car)
        {
            _carRepo.UpdateCarDetails(carId, car);
        }
        public void DeleteCar(int carId)
        {
            _carRepo.DeleteCar(carId);
        }

    }
}
