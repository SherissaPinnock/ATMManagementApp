using ATMManagementApp.Pages.Investigations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ATMManagementApp.Pages.Investigations.InvestIndexModel;

namespace ATMManagementApp.Pages.Auditor
{
    public class PastModel : PageModel
    {//List for storing data on investigation
        public List<InvestInfo> pastInvests = new List<InvestInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-QLJKDBL\\SQLEXPRESS;Initial Catalog=FinancialServicesCommisson;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select * FROM PastInvestigationsReport";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                InvestInfo investInfo = new InvestInfo("transactionCode", "transactionStatus", "transactionType", "transactionDate", "amount", "cardNumber", "accountNumber", "referenceNum", "purpose", "startDate","endDate","auditor");

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
                                investInfo.investigationEndDate = reader.GetDateTime(10).ToString();
                                investInfo.auditorName = reader.GetString(11);



                                //add ATM to list
                                pastInvests.Add(investInfo);
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
    }
}
