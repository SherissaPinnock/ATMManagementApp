using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ATMManagementApp.Pages.ATMs.IndexModel;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Net;
using System.Xml.Linq;

namespace ATMManagementApp.Pages.Admin
{
    public class FIModel : PageModel
    {
        //creates list for ATMs
        public List<FIInfo> listFIs = new List<FIInfo>();
        public List<FIInfo> listInvests = new List<FIInfo>();
        public void OnGet()
        {
            //InvestigationsPerFinancialInstitution
            try
            {
                String connectionString = "Data Source=DESKTOP-QLJKDBL\\SQLEXPRESS;Initial Catalog=FinancialServicesCommisson;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select * FROM InvestigationsPerFinancialInstitution";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FIInfo fiInfo = new FIInfo("FIName", "totalInvest");

                                fiInfo.FIName = reader.IsDBNull(0) ? null : reader.GetString(0);
                                fiInfo.totalInvest = reader.GetInt32(1).ToString();


                                //add ATM to list
                                listInvests.Add(fiInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }



            //FinancialInstitutionOverview View
            try
            {
                String connectionString = "Data Source=DESKTOP-QLJKDBL\\SQLEXPRESS;Initial Catalog=FinancialServicesCommisson;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select * FROM FinancialInstitutionOverview";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FIInfo fiInfo = new FIInfo("FINumber", "FIName", "address", "mainBanker", "branchNo",
                                 "contactNum", "deposit", "revenue");

                                fiInfo.FINumber = reader.IsDBNull(0) ? null : reader.GetInt32(0).ToString();
                                fiInfo.FIName = reader.IsDBNull(1) ? null : reader.GetString(1);
                                fiInfo.address = reader.IsDBNull(2) ? null : reader.GetString(2);
                                fiInfo.mainBanker = reader.IsDBNull(3) ? null : reader.GetString(3);
                                fiInfo.branchNo= reader.IsDBNull(3) ? null : reader.GetInt32(4).ToString();
                                fiInfo.contactNum = reader.IsDBNull(4) ? null : reader.GetInt64(5).ToString();
                                fiInfo.deposit = reader.IsDBNull(5) ? null : reader.GetDecimal(6).ToString();
                                fiInfo.revenue = reader.IsDBNull(6) ? null : reader.GetDecimal(7).ToString();
                               

                                //add ATM to list
                                listFIs.Add(fiInfo);
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


    public class FIInfo
    {
        public string FINumber;
        public string FIName;
        public string address;
        public string mainBanker;
        public string branchNo;
        public string contactNum;
        public string deposit;
        public string revenue;
        public string shareholders;
        public string totalInvest;

        public FIInfo( string FINumber, string FIName, string address, string mainBanker, string branchNo, 
            string contactNum, string deposit, string revenue)
        { 
            this.FINumber = FINumber;
            this.FIName = FIName;
            this.address = address;
            this.mainBanker = mainBanker;
            this.branchNo = branchNo;
            this.contactNum = contactNum;
            this.deposit = deposit;
            this.revenue = revenue;
            //this.shareholders = shareholders;
        }

        public FIInfo(string FIName, string totalInvest)
        {
            this.FIName= FIName;
            this.totalInvest= totalInvest;
        }

    }
}

