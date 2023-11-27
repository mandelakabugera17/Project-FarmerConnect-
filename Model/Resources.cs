using Microsoft.VisualBasic;

namespace FarmerConnect.Model
{
    public class Resources
    {
        public int? resource_id {  get; set; }
        public int? seller_id { get; set; }
        public string? name { get; set; }
        public int? quantity { get; set; }
        public decimal? price { get; set; }
        public string? description { get; set; }
        public DateAndTime? timestamp { get; set; }

        public Resources()
        {
        }

        public Resources(int? resource_id, int? seller_id, string? name, int? quantity, decimal? price, string? description, DateAndTime? timestamp)
        {
            this.resource_id = resource_id;
            this.seller_id = seller_id;
            this.name = name;
            this.quantity = quantity;
            this.price = price;
            this.description = description;
            this.timestamp = timestamp;
        }
    }
}
