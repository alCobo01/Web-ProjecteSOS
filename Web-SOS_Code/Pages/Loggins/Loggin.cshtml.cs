using Web_SOS_Code.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web_SOS_Code.Pages.Loggins
{
    public class LogginModel : PageModel
    {
        public User Loggin { get; set; } = new User();
        public void OnGet()
        {
            Loggin = new User();
        }
    }
}