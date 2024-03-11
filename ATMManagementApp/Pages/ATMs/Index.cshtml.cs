using ATMManagementApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Claims;


namespace ATMManagementApp.Pages.ATMs
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        //creates list for ATMs
        public List<ATMInfo> listATMs = new List<ATMInfo>();
        /* public void OnGet()
         {
             try
             {
                 String connectionString = "Data Source=DESKTOP-QLJKDBL\\SQLEXPRESS;Initial Catalog=FinancialServicesCommisson;Integrated Security=True;";

                 using (SqlConnection connection= new SqlConnection(connectionString))
                 {
                     connection.Open();
                     String sql = "Select * FROM ATM_Transaction_View";
                     using (SqlCommand command = new SqlCommand(sql, connection))
                     {
                         using (SqlDataReader reader = command.ExecuteReader())
                         {
                             while(reader.Read())
                             {

                                 ATMInfo atmInfo = new ATMInfo("atmCode", "FIName", "atmName", "atmAddress", "atmType", "totalTrans", "totalInvest");

                                 atmInfo.atmCode = reader.GetInt32(0).ToString(); 
                                 atmInfo.FIName = reader.GetString(1);
                                 atmInfo.atmName = reader.GetString(2);
                                 atmInfo.atmAddress = reader.GetString(3);
                                 atmInfo.atmType = reader.GetString(4);
                                 atmInfo.totalTrans= reader.GetInt32(5).ToString();
                                 atmInfo.totalInvest= reader.GetInt32(6).ToString();

                                 // Print values to console for debugging
                                 Console.WriteLine($"atmCode: {atmInfo.atmCode}, FIName: {atmInfo.FIName}, atmName: {atmInfo.atmName}, atmAddress: {atmInfo.atmAddress}, atmType: {atmInfo.atmType}, totaltrans: {atmInfo.totalTrans}, Invest: {atmInfo.totalInvest}");
                                 Debug.WriteLine($"atmCode: {atmInfo.atmCode}, FIName: {atmInfo.FIName}, atmName: {atmInfo.atmName}, atmAddress: {atmInfo.atmAddress}, atmType: {atmInfo.atmType}");

                                 //add ATM to list
                                 listATMs.Add(atmInfo);
                             }
                         }
                     }
                 }
             }
             catch(Exception ex)
             {
                 Console.WriteLine("Exception: " + ex.ToString());
             }
         }
     }*/
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

                    // Now you have the bank name of the logged-in user
                    // Use loggedInBankName in your code to filter the data
                }
            }

            try
            {
               
                // Construct the SQL query with a WHERE clause to filter by bank name
                string sql = @"SELECT
                    atm.atmCode,
                    atm.atmName AS atmName,
                    atm.atmAddress AS atmLocation,
                    atm.atmType AS atmType,
                    COUNT(tr.transactionCode) AS totalTransactions,
                    COUNT(DISTINCT iv.referenceNum) AS totalInvestigations
                FROM
                    ATM atm
                LEFT JOIN
                    ATMTransaction tr ON atm.atmCode = tr.atmCode
                LEFT JOIN
                    TransInvest tim ON tr.transactionCode = tim.transactionCode AND tr.atmCode = tim.atmCode
                LEFT JOIN
                    Investigation iv ON tim.referenceNum = iv.referenceNum
                LEFT JOIN
                    FinancialInstitution fi ON atm.FINumber = fi.FINumber
                WHERE
                    fi.name = @BankName -- Filter by bank name
                GROUP BY
                    atm.atmCode, atm.atmName, atm.atmAddress, atm.atmType;";

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
                              ATMInfo atmInfo = new ATMInfo("atmCode", "FIName", "atmName", "atmAddress", "atmType", "totalTrans", "totalInvest");

                                atmInfo.atmCode = reader.GetInt32(0).ToString();
                                atmInfo.FIName = loggedInBankName; // Assuming the bank name is the same for all ATMs
                                atmInfo.atmName = reader.GetString(1);
                                atmInfo.atmAddress = reader.GetString(2);
                                atmInfo.atmType = reader.GetString(3);
                                atmInfo.totalTrans = reader.GetInt32(4).ToString();
                                atmInfo.totalInvest = reader.GetInt32(5).ToString();
                                

                                // Add ATMInfo object to the list
                                listATMs.Add(atmInfo);
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


        public class ATMInfo
        {
            //Stores info on individual ATM
            public String atmCode;
            public String FIName;
            public String atmName;
            public String atmAddress;
            public String atmType;
            public String totalTrans;
            public String totalInvest;
            public String FINumber;

            // Constructor for displaying view
            public ATMInfo(String atmCode, String FIName, String atmName, String atmAddress, String atmType, String totalTrans, String totalInvest)
            {
                this.atmCode = atmCode;
                this.FIName = FIName;
                this.atmName = atmName;
                this.atmAddress = atmAddress;
                this.atmType = atmType;
                this.totalTrans = totalTrans;
                this.totalInvest = totalInvest;
            }

            public ATMInfo(String FINumber, String atmName, String atmAddress, String atmType)
            {
                this.FINumber = FINumber;
                this.atmName = atmName;
                this.atmAddress = atmAddress;
                this.atmType = atmType;
            }
        }
    }
}
