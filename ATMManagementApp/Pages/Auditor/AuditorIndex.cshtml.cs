using ATMManagementApp.Pages.ATMs;
using ATMManagementApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Transactions;

namespace ATMManagementApp.Pages.Investigations
{
    [Authorize(Roles = "auditor")]
    public class InvestIndexModel : PageModel
    {
        //List for storing data on investigation
        public List<InvestInfo> listInvests = new List<InvestInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-QLJKDBL\\SQLEXPRESS;Initial Catalog=FinancialServicesCommisson;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select * FROM OngoingInvestigationsReport";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                InvestInfo investInfo = new InvestInfo("transactionCode", "transactionStatus", "transactionType", "transactionDate", "amount", "cardNumber", "accountNumber", "referenceNum", "purpose", "startDate", "auditor");

                                investInfo.transactionCode = reader.GetInt32(0).ToString();
                                investInfo.transactionStatus = reader.GetString(1);
                                investInfo.transactionType = reader.GetString(2);
                                investInfo.transactionDate = reader.GetDateTime(3).ToString();
                                investInfo.amount = reader.GetDecimal(4).ToString();
                                investInfo.cardNumber = reader.GetString(5);
                                investInfo.accountNumber = reader.GetString(6);
                                investInfo.referenceNum = reader.GetInt32(7).ToString();
                                investInfo.purpose = reader.GetString(8);
                                investInfo.investigationStartDate = reader.GetDateTime(9).ToString();
                                investInfo.auditorName = reader.GetString(10);



                                //add ATM to list
                                listInvests.Add(investInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }




        /*public IActionResult SearchInvestigation(string referenceNumber)
        {
            // Replace "YourDbContext" with your actual DbContext class name
            using (var context = new ApplicationDBContext())
            {
                var investigations = context.InvestigationReport
                                            .FromSqlInterpolated($"SELECT * FROM OngoingInvestigationsReport WHERE referenceNum = {referenceNumber}")
                                            .ToList();

                return View("Investigations", investigations);
            }
        }*/

        /*private IActionResult View(string v, object investigations)
        {
            throw new NotImplementedException();
        }*/
    

        //Constructor for view
        public class InvestInfo
        {
            public string transactionCode;
            public string transactionStatus;
            public string transactionType;
            public string transactionDate;
            public string amount;
            public string cardNumber;
            public string accountNumber;
            public string purpose;
            public string referenceNum;
            public string investigationStartDate;
            public string investigationEndDate;
            public string auditorName;
            public string auditorID;
            public string atmCode;

            //Default Constructor
            public InvestInfo(String transactionCode, String transactionStatus, String transactionType, String transactionDate
                , String amount, String cardNumber, String accountNumber, String purpose, String referenceNum, String investigationStartDate, String
                investigationEndDate, String auditorName)
            {
                this.transactionCode = transactionCode;
                this.transactionStatus = transactionStatus;
                this.transactionType = transactionType;
                this.transactionDate = transactionDate;
                this.amount = amount;
                this.cardNumber = cardNumber;
                this.accountNumber = accountNumber;
                this.purpose = purpose;
                this.referenceNum = referenceNum;
                this.investigationStartDate = investigationStartDate;
                this.investigationEndDate = investigationEndDate;
                this.auditorName = auditorName;
            }

            //Constructor for ongoing view
            public InvestInfo(String transactionCode, String transactionStatus, String transactionType, String transactionDate
                , String amount, String cardNumber, String accountNumber, String purpose, String referenceNum, String investigationStartDate, String auditorName)
            {
                this.transactionCode = transactionCode;
                this.transactionStatus = transactionStatus;
                this.transactionType = transactionType;
                this.transactionDate = transactionDate;
                this.amount = amount;
                this.cardNumber = cardNumber;
                this.accountNumber = accountNumber;
                this.purpose = purpose;
                this.referenceNum = referenceNum;
                this.investigationStartDate = investigationStartDate;
                this.auditorName = auditorName;
            }
            public InvestInfo(String referenceNum, String status)
            {
                this.referenceNum = referenceNum;
                this.transactionStatus = status;
            }

            //Constructor for creating new investigation
            public InvestInfo(String atmCode, String transactionCode, String purpose, String auditorID)
            {
                this.atmCode = atmCode;
                this.transactionCode = transactionCode;
                this.purpose = purpose;
                this.auditorID = auditorID;
            }
        }



    }
}
