namespace FarmerConnect.Model
{
    public class Transaction
    {
        public int? transaction_id { get; set; }
        public int? buyer_id { get; set; }
        public int? seller_id { get; set; }
        public int? resource_id { get; set; }
        public int? service_id { get; set; }
        public int? quantity { get; set; }
        public decimal? total_amount { get; set; }
        public DateTime? transaction_date { get; set; }

        public Transaction()
        {
        }

        public Transaction(int? transaction_id, int? buyer_id, int? seller_id, int? resource_id, int? service_id, int? quantity, decimal? total_amount, DateTime? transaction_date)
        {
            this.transaction_id = transaction_id;
            this.buyer_id = buyer_id;
            this.seller_id = seller_id;
            this.resource_id = resource_id;
            this.service_id = service_id;
            this.quantity = quantity;
            this.total_amount = total_amount;
            this.transaction_date = transaction_date;
        }
    }
}
