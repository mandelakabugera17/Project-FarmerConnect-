using FarmerConnect.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FarmerConnect.Pages
{
    public class AgribusinessesInfoModel : PageModel
    {
        String stgcon = "Data Source=DESKTOP-8UTAP68\\SQLEXPRESS;Initial Catalog=FarmerConnect;Integrated Security=True";
        public string message = "";
        Agribusinesses agribusinesses = new Agribusinesses();

        public void OnGet()
        {
        }
        public void OnPost() {
            try
            {
                agribusinesses.userid = int.Parse(Request.Form["user_id"]);
                agribusinesses.bussiness_name = Request.Form["bussiness"];
                agribusinesses.location = Request.Form["location"];
                agribusinesses.services_offered = Request.Form["service"];

            }
            catch (Exception ex)
            {
                message = "Problem: " + ex.Message;
            }
            using (SqlConnection con = new SqlConnection(stgcon))
            {
                string query = "INSERT INTO Agribusinesses(user_id, business_name, location, services_offered) VALUES(@user_id, @business_name, @location, @services_offered)";

                try
                {

                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@user_id", agribusinesses.userid);
                        cmd.Parameters.AddWithValue("@business_name", agribusinesses.bussiness_name);
                        cmd.Parameters.AddWithValue("@location", agribusinesses.location);
                        cmd.Parameters.AddWithValue("@services_offered", agribusinesses.services_offered);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            message = "Agribusinesses Saved";
                            agribusinesses = new Agribusinesses();
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
