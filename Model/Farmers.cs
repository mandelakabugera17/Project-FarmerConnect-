namespace FarmerConnect.Model
{
    public class Farmers
    {
        public int? Id { get; set; }
        public int? userid { get; set; }
        public int? farmsize { get; set; }
        public string? location { get; set; }
        public string? crop { get; set; }
        public string? membership_status { get; set; }

        public Farmers()
        {
        }

        public Farmers(int id, int userid, int farmsize, string location, string crop, string membership_status)
        {
            Id = id;
            this.userid = userid;
            this.farmsize = farmsize;
            this.location = location;
            this.crop = crop;
            this.membership_status = membership_status;
        }
    }
}
