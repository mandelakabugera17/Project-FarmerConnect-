using FarmerConnect.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace FarmerConnect.Pages
{
    public class TransactionModel : PageModel
    {
        String stgcon = "Data Source=DESKTOP-8UTAP68\\SQLEXPRESS;Initial Catalog=FarmerConnect;Integrated Security=True";
        Transaction transaction = new Transaction();
        public string message = "";
        private readonly ILogger<TransactionModel> _logger;

        public TransactionModel(ILogger<TransactionModel> logger)
        {
            _logger = logger;
        }


        public void OnGet(int? serviceId, int? sellerId, int? resources_id, decimal? price, int? quantity)
        {
            ViewData["ServiceId"] = serviceId;
            ViewData["resources_id"] = resources_id;
            ViewData["SellerId"] = sellerId;
            ViewData["Price"] = price;
            ViewData["Quantity"] = quantity;

        }
        public IActionResult OnPost(int? serviceId)
        {
            if (serviceId.HasValue)
            {
                try
                {
                    transaction.buyer_id = HttpContext.Session.GetInt32("UserId");
                    transaction.seller_id = int.Parse(Request.Form["sellerId"]);
                    transaction.service_id = int.Parse(Request.Form["serviceId"]);
                    transaction.total_amount = decimal.Parse(Request.Form["totalAmount"]);
                }
                catch (Exception ex)
                {
                    message = "There's a problemo: " + ex.Message;
                    return Page();
                }


                if (transaction.total_amount.HasValue && transaction.total_amount > 0)
                {
                    using (SqlConnection con = new SqlConnection(stgcon))
                    {
                        string query = "INSERT INTO Transactions (buyer_id, seller_id, service_id, total_amount) VALUES (@buyer_id, @seller_id, @service_id, @total_amount)";

                        try
                        {
                            con.Open();

                            using (SqlCommand cmd = new SqlCommand(query, con))
                            {
                                cmd.Parameters.AddWithValue("@buyer_id", transaction.buyer_id);
                                cmd.Parameters.AddWithValue("@seller_id", transaction.seller_id);
                                cmd.Parameters.AddWithValue("@service_id", transaction.service_id);
                                cmd.Parameters.AddWithValue("@total_amount", transaction.total_amount);

                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    message = "Sucessfull Transaction";
                                    transaction = new Transaction();
                                }
                                else
                                {
                                    message = "failed";
                                }
                            }

                            return RedirectToPage("/FarmerHome");
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
                    message = "Invalid total amount";
                    return Page();
                }
            }
            else
            {
                try
                {
                    transaction.buyer_id = HttpContext.Session.GetInt32("UserId");
                    transaction.seller_id = int.Parse(Request.Form["sellerId"]);
                    transaction.resource_id = int.Parse(Request.Form["resourceId"]);
                    transaction.quantity = int.Parse(Request.Form["quantity"]);
                    transaction.total_amount = decimal.Parse(Request.Form["totalAmount"]);

                }
                catch (Exception ex)
                {
                    message = "There's a problemu: " + ex.Message;
                    return Page();
                }


                if (transaction.total_amount.HasValue && transaction.total_amount > 0)
                {
                    using (SqlConnection con = new SqlConnection(stgcon))
                    {
                        string query = "INSERT INTO Transactions (buyer_id, seller_id, resource_id, quantity, total_amount) VALUES (@buyer_id, @seller_id, @resource_id, @quantity, @total_amount)";

                        try
                        {
                            con.Open();

                            using (SqlCommand cmd = new SqlCommand(query, con))
                            {
                                cmd.Parameters.AddWithValue("@buyer_id", transaction.buyer_id);
                                cmd.Parameters.AddWithValue("@seller_id", transaction.seller_id);
                                cmd.Parameters.AddWithValue("@resource_id", transaction.resource_id);
                                cmd.Parameters.AddWithValue("@quantity", transaction.quantity);
                                cmd.Parameters.AddWithValue("@total_amount", transaction.total_amount);

                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    UpdateResourceQuantity(transaction.resource_id, transaction.quantity);
                                    message = "Sucessfull Transaction";
                                    transaction = new Transaction();
                                }
                                else
                                {
                                    message = "failed";
                                }
                            }





                            return RedirectToPage("/FarmerHome");
                        }
                        catch (Exception ex)
                        {
                            message = "There's a problema: " + ex.Message;
                            return Page();
                        }
                    }
                }
                else
                {
                    message = "Invalid total amount";
                    return Page();
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
