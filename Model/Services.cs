using Microsoft.VisualBasic;

namespace FarmerConnect.Model
{
    public class Services
    {

        public int ServiceId { get; set; }
        public int? ProviderId { get; set; }
        public string ServiceType { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Timestamp { get; set; }

        public Services()
        {
        }

        public Services(int serviceId, int? providerId, string serviceType, string description, decimal price, DateTime timestamp)
        {
            ServiceId = serviceId;
            ProviderId = providerId;
            ServiceType = serviceType;
            Description = description;
            Price = price;
            Timestamp = timestamp;
        }
    }
}

