namespace FarmerConnect.Model
{
    public class Users
    {
        public int? userid {  get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? email { get; set; }
        public string? user_type { get; set;}

        public Users()
        {
        }

        public Users(int? userid, string? username, string? password, string? email, string? user_type)
        {
            this.userid = userid;
            this.username = username;
            this.password = password;
            this.email = email;
            this.user_type = user_type;
        }

    }
}
