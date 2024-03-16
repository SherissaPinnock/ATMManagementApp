using ATMManagementApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Claims;
using static ATMManagementApp.Pages.ATMs.IndexModel;
using static ATMManagementApp.Pages.Investigations.InvestIndexModel;

namespace ATMManagementApp.Pages.ATMs
{
    public class TransactionsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionsModel(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        //creates list for Transactions
        public List<InvestInfo> myTransactions = new List<InvestInfo>();
        public async Task<IActionResult> OnGet()
        {
            string loggedInBankName = null;
            // Retrieve the current user's ID
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                // Retrieve the user from the UserManager
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    // Assuming you have a property named BankName in your ApplicationUser model
                    loggedInBankName = user.BankName;
                    Console.WriteLine("Logged in Name: " + loggedInBankName);
                    // Now you have the bank name of the logged-in user
                    // Use loggedInBankName in your code to filter the data
                }
            }

            try
            {

                // Construct the SQL query with a WHERE clause to filter by bank name
                string sql = "SELECT\r\n    tr.transactionCode,\r\n    tr.status AS transactionStatus,\r\n    tr.type AS transactionType,\r\n  tr.amount,\r\n    tr.accountNumber,\r\n    iv.purpose AS investigationPurpose,\r\n    iv.dateStarted AS investigationStartDate\r\nFROM\r\n    ATMTransaction tr\r\nJOIN\r\n    TransInvest tim ON tr.transactionCode = tim.transactionCode\r\nJOIN\r\n    Investigation iv ON tim.referenceNum = iv.referenceNum\r\nJOIN\r\n    ATM a ON tr.atmCode = a.atmCode\r\nJOIN\r\n    FinancialInstitution fi ON a.FINumber = fi.FINumber -- Assuming FINumber is the common identifier\r\nWHERE\r\n    fi.name = @BankName;";


                // Connect to the database and execute the query
                String connectionString = "Data Source=DESKTOP-QLJKDBL\\SQLEXPRESS;Initial Catalog=FinancialServicesCommisson;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Set the parameter value for the bank name
                        command.Parameters.AddWithValue("@BankName", loggedInBankName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Retrieve ATM information from the reader and populate ATMInfo objects
                                InvestInfo investInfo = new InvestInfo("transactionCode", "transactionStatus", "transactionType", "amount", "accountNumber", "purpose", "investigationStartDate");

                                investInfo.transactionCode = reader.GetInt32(0).ToString();
                                investInfo.transactionStatus = reader.GetString(1); // Assuming the bank name is the same for all ATMs
                                investInfo.transactionType = reader.GetString(2);
                                investInfo.amount = reader.GetDecimal(3).ToString();
                                investInfo.accountNumber = reader.GetString(4);
                                investInfo.purpose = reader.GetString(5);
                                investInfo.investigationStartDate = reader.GetDateTime(6).ToString();
                               

                                // Add ATMInfo object to the list
                                myTransactions.Add(investInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Logged in Name: " + loggedInBankName);
                Console.WriteLine("Exception: " + ex.ToString());

            }

            return Page();
        }

    }
}
