namespace Assignment__Car_Rental_System.Services
{
    public interface INotificationService
    {
        public void SendNotification(string receiverEmail, string receiverName, string carMake, string carModel, int noOfDays);
    }
}
