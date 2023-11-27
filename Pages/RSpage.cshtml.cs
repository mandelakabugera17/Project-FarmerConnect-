using FarmerConnect.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FarmerConnect.Pages
{
    public class RSpageModel : PageModel
    {
        String stgcon = "Data Source=DESKTOP-8UTAP68\\SQLEXPRESS;Initial Catalog=FarmerConnect;Integrated Security=True";
        public string message = "";
        private readonly ILogger<RSpageModel> _logger;

        public Resources Resources = new Resources();
        public Services Services = new Services();

        public RSpageModel(ILogger<RSpageModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int? serviceId, int? sellerId, int? resources_id, decimal? Price, int? quantity)
        {
            ViewData["ServiceId"] = serviceId;
            ViewData["resources_id"] = resources_id;
            ViewData["SellerId"] = sellerId;
            ViewData["Price"] = Price;
            ViewData["Quantity"] = quantity;

        }

        public IActionResult OnPost(int? serviceId)
        {
            if (serviceId.HasValue)
            {
                try
                {

                    Services.ServiceId = int.Parse(Request.Form["ServiceId"]);
                    Services.Price = int.Parse(Request.Form["price"]);

                }
                catch (Exception ex)
                {
                    message = "There's a problemo: " + ex.Message;
                    return Page();
                }

                    using (SqlConnection con = new SqlConnection(stgcon))
                    {
                        string query = "UPDATE Services SET Price=@price WHERE service_id=@serviceid ";

                        try
                        {
                            con.Open();

                            using (SqlCommand cmd = new SqlCommand(query, con))
                            {
                                cmd.Parameters.AddWithValue("@serviceid", Services.ServiceId);
                                cmd.Parameters.AddWithValue("@price", Services.Price);

                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    message = "Sucessfull Transaction";
                                    Services = new Services();
                                }
                                else
                                {
                                    message = "failed"; int.Parse(Request.Form["sellerId"]);
                            }
                            }

                            return RedirectToPage("/Report");
                        }
                        catch (Exception ex)
                        {
                            message = "There's a problemi: " + ex.Message;
                            return Page();
                        }
                    }
                
            }
            else
            {
                try
                {
                    Resources.resource_id = int.Parse(Request.Form["resource_id"]);
                    Resources.price = int.Parse(Request.Form["price"]);

                }
                catch (Exception ex)
                {
                    message = "There's a problemu: " + ex.Message;
                    return Page();
                }


                    using (SqlConnection con = new SqlConnection(stgcon))
                    {
                    string query = "UPDATE Resources SET Price=@price WHERE resource_id=@resourceid ";

                    try
                        {
                            con.Open();

                            using (SqlCommand cmd = new SqlCommand(query, con))
                            {
                                cmd.Parameters.AddWithValue("@resourceid", Resources.resource_id);
                                cmd.Parameters.AddWithValue("@price", Resources.price);

                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    message = "Sucessfull Update";
                                    Resources = new Resources();
                                }
                                else
                                {
                                    message = "failed";
                                }
                            }





                            return RedirectToPage("/Report");
                        }
                        catch (Exception ex)
                        {
                            message = "There's a problema: " + ex.Message;
                            return Page();
                        }
                    }
            }
        }

        private void UpdateResourceQuantity(int? resourceId, int? purchasedQuantity)
        {
            using (SqlConnection con = new SqlConnection(stgcon))
            {
                string query = "UPDATE Resources SET quantity = quantity - @purchasedQuantity WHERE resource_id = @resourceId";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@resourceId", resourceId);
                    cmd.Parameters.AddWithValue("@purchasedQuantity", purchasedQuantity);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
