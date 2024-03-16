using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ATMManagementApp.Pages.Investigations.InvestIndexModel;

namespace ATMManagementApp.Pages.Admin
{
    [Authorize(Roles = "admin")]
    public class AdminIndexModel : PageModel

    {
        public List<InvestInfo> transList = new List<InvestInfo>();
        //private readonly string _connectionString= "Data Source=DESKTOP-QLJKDBL\\\\SQLEXPRESS;Initial Catalog=FinancialServicesCommisson;Integrated Security=True;";
        public int NumberOfTransactions { get; set; }
        public int NumberOfFIs { get; set; }
        public int NumberOfFailed { get; set; }

        public int NumberOfATMs { get; set; }

        public int GetNumberOfTransactions()
        {
            int numberOfTransactions = 0;
            String _connectionString = "Data Source=DESKTOP-QLJKDBL\\SQLEXPRESS;Initial Catalog=FinancialServicesCommisson;Integrated Security=True;";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = "SELECT COUNT(*) FROM ATMTransaction";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        numberOfTransactions = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Exception while retrieving number of transactions: " + ex.Message);
            }

            return numberOfTransactions;
            Console.WriteLine(numberOfTransactions);
        } //End og getTransactions()

        public int GetTotalFIs()
        {
            int totalFIs = 0;
            String _connectionString = "Data Source=DESKTOP-QLJKDBL\\SQLEXPRESS;Initial Catalog=FinancialServicesCommisson;Integrated Security=True;";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = "SELECT COUNT(*) FROM FinancialInstitution";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        totalFIs = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine(ex.Message);
            }

            return totalFIs;
        }

        public int GetFailedTransactions()
        {
            String _connectionString = "Data Source=DESKTOP-QLJKDBL\\SQLEXPRESS;Initial Catalog=FinancialServicesCommisson;Integrated Security=True;";

            int failedTransactions = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = "SELECT COUNT(*) FROM ATMTransaction WHERE status = 'Failed'";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        failedTransactions = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine(ex.Message);
            }

            return failedTransactions;
        }

        //Method that gets the total ATMs
        public int GetTotalATMs()
        {
            int totalATMs = 0;
            String _connectionString = "Data Source=DESKTOP-QLJKDBL\\SQLEXPRESS;Initial Catalog=FinancialServicesCommisson;Integrated Security=True;";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = "SELECT COUNT(*) FROM ATM";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        totalATMs = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine(ex.Message);
            }

            return totalATMs;
        }


        public void OnGet()
        {
            NumberOfTransactions = GetNumberOfTransactions();
            NumberOfFIs = GetTotalFIs();
            NumberOfFailed = GetFailedTransactions();
            NumberOfATMs = GetTotalATMs();

            //For the table:
            try
            {
                String connectionString = "Data Source=DESKTOP-QLJKDBL\\SQLEXPRESS;Initial Catalog=FinancialServicesCommisson;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select * FROM RecentTransactionsView";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                InvestInfo investInfo = new InvestInfo("accountNumber", "transactionDate", "Amount", "status");

                                investInfo.accountNumber = reader.GetString(0);
                                investInfo.transactionDate = reader.GetDateTime(1).ToString();
                                investInfo.amount = reader.GetDecimal(2).ToString();
                                investInfo.transactionStatus = reader.GetString(3);



                                //add ATM to list
                                transList.Add(investInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
