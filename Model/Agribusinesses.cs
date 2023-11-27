namespace FarmerConnect.Model
{
    public class Agribusinesses
    {
        public int? id {  get; set; }
        public int? userid { get; set; }
        public string? bussiness_name { get; set; }
        public string? location { get; set; }
        public string? services_offered { get; set; }
        public string? membership_status { get; set; }

        public Agribusinesses()
        {
        }

        public Agribusinesses(int? id, int? userid, string? bussiness_name, string? location, string? services_offered, string? membership_status)
        {
            this.id = id;
            this.userid = userid;
            this.bussiness_name = bussiness_name;
            this.location = location;
            this.services_offered = services_offered;
            this.membership_status = membership_status;
        }
    }
}
