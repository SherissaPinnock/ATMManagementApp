using ATMManagementApp.Pages.ATMs;
using ATMManagementApp.Pages.Investigations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using static ATMManagementApp.Pages.Investigations.InvestIndexModel;

namespace ATMManagementApp.Pages.Auditor
{
    public class OpenModel : PageModel
    {
        public InvestInfo investInfo = new InvestInfo("atmCode", "transactionCode", "purpose", "auditorID");
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            investInfo.atmCode = Request.Form["atmCode"];
            investInfo.transactionCode = Request.Form["transactionCode"];
            investInfo.purpose = Request.Form["purpose"];
            investInfo.auditorID = Request.Form["auditorID"];

            if (investInfo.atmCode.Length == 0 || investInfo.purpose.Length == 0 ||
                investInfo.transactionCode.Length == 0 || investInfo.auditorID.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-QLJKDBL\\SQLEXPRESS;Initial Catalog=FinancialServicesCommisson;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create SqlCommand for the stored procedure
                    using (SqlCommand command = new SqlCommand("SP_InsertInvestigation", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@ATMCode", int.Parse(investInfo.atmCode));
                        command.Parameters.AddWithValue("@TransactionCode", int.Parse(investInfo.transactionCode));
                        command.Parameters.AddWithValue("@Purpose", investInfo.purpose);
                        command.Parameters.AddWithValue("@AuditorID", int.Parse(investInfo.auditorID));

                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                        Debug.WriteLine($"ATMCode: {investInfo.atmCode}, atmName: {investInfo.transactionCode}, purpose: {investInfo.purpose}, auditorID:{investInfo.auditorID}");
                        Console.WriteLine($"ATMCode: {investInfo.atmCode}, atmName: {investInfo.transactionCode}, purpose: {investInfo.purpose}, auditorID:{investInfo.auditorID}");
                    }
                }

                // If execution reaches here, the stored procedure was executed successfully
                successMessage = "New Investigation added successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
                errorMessage = "Failed to add investigation. Please try again.";
            }

            // Redirect the user
            Response.Redirect("/Auditor/AuditorIndex");
        }

    }
}

