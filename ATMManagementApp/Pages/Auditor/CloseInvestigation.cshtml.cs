using ATMManagementApp.Pages.ATMs;
using ATMManagementApp.Pages.Investigations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using static ATMManagementApp.Pages.Investigations.InvestIndexModel;

namespace ATMManagementApp.Pages.Auditor
{
    public class CloseInvestigationModel : PageModel
    {
        public InvestInfo investInfo = new InvestInfo("referenceNum", "status");
        public String errorMessage = "";
        public String successMessage = "";

        [BindProperty]
        public int referenceNum { get; set; }

        [BindProperty]
        public string status { get; set; }

        private readonly string _connectionString = "Data Source=DESKTOP-QLJKDBL\\SQLEXPRESS;Initial Catalog=FinancialServicesCommisson;Integrated Security=True;";


        public void OnGet()
        {
           
        }

        public IActionResult OnPost()
        {
            Console.WriteLine("Reference Number: " + Request.Form["referenceNum"]);
            Console.WriteLine("Status: " + Request.Form["status"]);
            try
            {
                // Create a SqlConnection object with the connection string
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SqlCommand object for calling the stored procedure
                    using (SqlCommand command = new SqlCommand("SP_CloseInvestigation", connection))
                    {
                        // Set the command type as stored procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the stored procedure
                        command.Parameters.AddWithValue("@ReferenceNum", referenceNum);
                        command.Parameters.AddWithValue("@Status", status);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Set success message
                        successMessage = "Investigation closed successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Set error message
                errorMessage = ex.Message;
                return Page(); // Return the page to display the error message
            }

            // Redirect to another page or return a success message
            return RedirectToPage("/Auditor/AuditorIndex"); // Redirect to the home page or any other page
        }
    }
}

