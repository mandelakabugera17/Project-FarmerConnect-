using FarmerConnect.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace FarmerConnect.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Session.Clear();
        }
    
    }
}

