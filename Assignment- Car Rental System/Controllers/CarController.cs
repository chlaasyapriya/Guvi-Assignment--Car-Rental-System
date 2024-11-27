using Assignment__Car_Rental_System.Models;
using Assignment__Car_Rental_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment__Car_Rental_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarRentalService carRentalService;
        public CarController(ICarRentalService carRentalService)
        {
            this.carRentalService = carRentalService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetAvailableCars() 
        {
            return carRentalService.GetAvailableCars().ToList();
        }

        [HttpGet("rentCar")]
        public async Task<IActionResult> RentCar(int carId, int userId, int noOdDays)
        {
            if (carRentalService.RentCar(carId, userId, noOdDays))
                return Ok("Car booked");
            return BadRequest("Car is not available");
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddCar(Car car)
        {
            carRentalService.AddCar(car);
            return NoContent();
        }

        [HttpPut("{carId}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateCar(int carId,Car car) 
        {
            if (carId != car.Id)
                return BadRequest("Car ID doesn't match with the car ID in the details.");
            carRentalService.UpdateCar(carId, car);
            return NoContent();
        }

        [HttpDelete("{carId}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteCar(int carId)
        {
            carRentalService.DeleteCar(carId);
            return NoContent();
        }

    }
}
