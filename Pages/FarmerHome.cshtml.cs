using FarmerConnect.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;  // Import List
using System.Data.SqlClient;

namespace FarmerConnect.Pages
{
    public class FarmerHomeModel : PageModel
    {
        private readonly string stgcon = "Data Source=DESKTOP-8UTAP68\\SQLEXPRESS;Initial Catalog=FarmerConnect;Integrated Security=True";
        private readonly Farmers farmers = new Farmers();
        public string message = "";

        public List<Farmers> Farmerslist = new List<Farmers>();

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
                    farmers.userid = userId.Value;

                    using (SqlConnection con = new SqlConnection(stgcon))
                    {
                        string query = "SELECT * FROM Farmers WHERE user_id=@user_id";
                        con.Open();

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@user_id", farmers.userid);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                   
                                    return RedirectToPage("/FarmersInfo");
                                }
                            }
                        }

                        con.Close();
                    }
                }
                else
                {
                   message = "User ID is not available in the session.";
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
