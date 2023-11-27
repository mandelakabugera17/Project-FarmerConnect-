using FarmerConnect.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FarmerConnect.Pages
{
    public class AgribusinessModel : PageModel
    {
        private readonly string stgcon = "Data Source=DESKTOP-8UTAP68\\SQLEXPRESS;Initial Catalog=FarmerConnect;Integrated Security=True";
        public Agribusinesses Agribusinesses = new Agribusinesses();
        public string message = "";
        public void OnGet()
        {
            getknowledgesharing();
        }
        public IActionResult getknowledgesharing()
        {
            try
            {
                int? userId = HttpContext.Session.GetInt32("UserId");

                if (userId.HasValue)
                {
                    Agribusinesses.userid = userId.Value;

                    using (SqlConnection con = new SqlConnection(stgcon))
                    {
                        string query = "SELECT * FROM Agribusinesses WHERE user_id=@user_id";
                        con.Open();

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@user_id", Agribusinesses.userid);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    message = "good";
                                }
                                else
                                {
                                    // Redirect to AgribusinessInfo page without specifying an area
                                    return RedirectToPage("/AgribusinessesInfo", new { area = "Pages" });
                                }
                            }
                        }

                        con.Close();
                    }
                }
                else
                {
                    message = "User ID is not available in the session.";
                    // Handle this case as needed
                    // For now, let's return a NotFound result
                    return NotFound();
                }

                return Page();
            }
            catch (Exception ex)
            {
                message = "Problem: " + ex.Message;
                // Handle this case as needed
                // For now, let's return a NotFound result
                return NotFound();
            }
        }


    }
}
