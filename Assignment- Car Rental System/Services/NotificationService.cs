using SendGrid;
using SendGrid.Helpers.Mail;

namespace Assignment__Car_Rental_System.Services
{
    public class NotificationService: INotificationService
    {
        private readonly string _apiKey;
        private readonly string _senderEmail;
        private readonly string _senderName;
        public NotificationService(IConfiguration iconfiguration)
        {
            _apiKey = iconfiguration["SendGrid:ApiKey"];
            _senderEmail = iconfiguration["SendGrid:SenderEmail"];
            _senderName = iconfiguration["SendGrid:SenderName"];
        }

        public void SendNotification(string receiverEmail,string receiverName, string carMake, string carModel, int noOfDays)
        {
            var client=new SendGridClient(_apiKey);
            var from = new EmailAddress(_senderEmail, _senderName);
            var subject = "Car Rental Booking Confirmation";
            var to=new EmailAddress(receiverEmail, receiverName);
            var plainTextContent = $"Hi {receiverName},\nYour booking for the car {carMake} {carModel} has been confirmed for {noOfDays} days.\nThankyou for using our service!";
            var htmlContent = $"<p>Hi {receiverName},</p><p>Your booking for the car {carMake} {carModel} has been confirmed for {noOfDays} days.</p><p>Thankyou for using our service!</p>";
            var message = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent,htmlContent);
            var response=client.SendEmailAsync(message);
            if (!response.IsCompletedSuccessfully)
                throw new Exception("Failed to send mail");
        }
    }
}
