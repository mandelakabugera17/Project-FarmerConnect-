using FarmerConnect.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FarmerConnect.Pages
{
    public class FarmersInfoModel : PageModel
    {
        String stgcon = "Data Source=DESKTOP-8UTAP68\\SQLEXPRESS;Initial Catalog=FarmerConnect;Integrated Security=True";
        public string message = "";
        Farmers farmers = new Farmers();

        public void OnGet()
        {
        }
        public void OnPost() {
            try
            {
                farmers.userid = int.Parse(Request.Form["user_id"]);
                farmers.farmsize = int.Parse(Request.Form["size"]);
                farmers.location = Request.Form["location"];
                farmers.crop = Request.Form["crop"];
                
            }
            catch (Exception ex)
            {
                message = "Problem: " + ex.Message;
            }
            using (SqlConnection con = new SqlConnection(stgcon))
            {
                string query = "INSERT INTO Farmers(user_id, farm_size, location, crops_grown) VALUES(@user_id, @farm_size, @location, @crops_grown)";

                try
                {

                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@user_id", farmers.userid);
                        cmd.Parameters.AddWithValue("@farm_size", farmers.farmsize);
                        cmd.Parameters.AddWithValue("@location", farmers.location);
                        cmd.Parameters.AddWithValue("@crops_grown", farmers.crop);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            message = "Farmer Saved";
                            farmers = new Farmers();
                        }
                        else
                        {
                            message = "Failed to save";
                        }
                    }
                    con.Close();

                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("conflicted with the FOREIGN KEY"))
                    {
                        message = "Your User ID is Not in the Database";
                    }
                    else
                    {
                        message = "Problem: " + ex.Message;
                    }

                }
            }

        }
    }
}
