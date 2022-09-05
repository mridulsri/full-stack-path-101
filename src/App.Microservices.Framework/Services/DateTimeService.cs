using App.Application.Interfaces;

namespace App.Microservices.Framework.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
