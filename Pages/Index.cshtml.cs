using FarmerConnect.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FarmerConnect.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        String stgcon = "Data Source=DESKTOP-8UTAP68\\SQLEXPRESS;Initial Catalog=FarmerConnect;Integrated Security=True";
        KnowledgeSharing knowledgesharing = new KnowledgeSharing();
        public List<KnowledgeSharing> knowledgesharingList = new List<KnowledgeSharing>();
        public void OnGet()
        {
            getknowledgesharing();
        }
        public void getknowledgesharing()
        {
            try
            {

                using (SqlConnection con = new SqlConnection(stgcon))
                {
                    string query = "SELECT post_id, title, content, timestamp FROM KnowledgeSharing";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                KnowledgeSharing knowledgesharing = new KnowledgeSharing();

                                knowledgesharing.Id = reader.GetInt32(0);
                                knowledgesharing.title = reader.GetString(1);
                                knowledgesharing.content = reader.GetString(2);
                                knowledgesharing.timestamp = reader.GetDateTime(3);

                                knowledgesharingList.Add(knowledgesharing);
                            }
                        }
                    }
                    con.Close();
                }

            }
            catch (Exception ex)
            {
               
            }
        }
    }
}