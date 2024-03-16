using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ATMManagementApp.Pages.ATMs.IndexModel;
using System.Diagnostics;
using static ATMManagementApp.Pages.Investigations.InvestIndexModel;
using System.Data.SqlClient;

namespace ATMManagementApp.Pages.Admin
{
    public class AuditorPageModel : PageModel
    {
        public List<Auditor> listAuditors = new List<Auditor>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-QLJKDBL\\SQLEXPRESS;Initial Catalog=FinancialServicesCommisson;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select * FROM ActiveAuditorDetailsView;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                Auditor auditor = new Auditor("auditorID", "auditorName", "auditorPhone", "ongoingInvest", "pastInvest");

                                auditor.auditorID = reader.GetInt32(0).ToString();
                                auditor.auditorName = reader.GetString(1);
                                auditor.auditorPhone = reader.GetString(2);
                                auditor.ongoingInvest = reader.GetInt32(3).ToString();
                                auditor.pastInvest = reader.GetInt32(4).ToString();


                                // Print values to console for debugging
                                Console.WriteLine($"ID: {auditor.auditorID}, Name: {auditor.auditorName}, Phone: {auditor.auditorPhone}, Ongoing: {auditor.ongoingInvest}, Past: {auditor.pastInvest}");
                                

                                //add ATM to list
                                listAuditors.Add(auditor);
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

        public class Auditor
        {
            public string auditorID { get; set; }
            public string auditorName { get; set; }

            public string auditorPhone { get; set; }

            public string ongoingInvest { get; set; }

            public string pastInvest { get; set; }

            public Auditor(String auditorID, String auditorName, String auditorPhone, String ongoingInvest, String pastInvest)
            {
                this.auditorID= auditorID;
                this.auditorName= auditorName;
                this.auditorPhone= auditorPhone;
                this.ongoingInvest= ongoingInvest;
                this.pastInvest= pastInvest;
            }
        }

       
    }
}
