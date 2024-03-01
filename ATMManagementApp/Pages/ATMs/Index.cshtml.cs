using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ATMManagementApp.Pages.ATMs
{
    public class IndexModel : PageModel
    {
        //creates list for ATMs
        public List<ATMInfo> listATMs = new List<ATMInfo>();
        public void OnGet()
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
