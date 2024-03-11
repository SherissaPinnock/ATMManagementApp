using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ATMManagementApp.Pages.Admin
{
    [Authorize(Roles="admin")]
    public class AdminIndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
