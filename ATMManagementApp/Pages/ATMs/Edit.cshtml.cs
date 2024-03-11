using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ATMManagementApp.Pages.ATMs.IndexModel;

namespace ATMManagementApp.Pages.ATMs
{
    public class EditModel : PageModel
    {
        public ATMInfo atmInfo = new ATMInfo("FINumber", "atmName", "atmAddress", "atmType");
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String atmCode = Request.Query["atmCode"];

            //connect to database
            try
            {

            }
            catch(Exception ex)
            {

            }
        }

        public void OnPost() 
        { 
        }
    }
}
