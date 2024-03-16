using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Query;
using System.Data;
using System.Data.SqlClient;
using static ATMManagementApp.Pages.Investigations.InvestIndexModel;

namespace ATMManagementApp.Pages.Admin
{
    public class AssignModel : PageModel
    {
        public List<InvestInfo> unassignedList = new List<InvestInfo>();
        public String errorMessage = "";
        public String successMessage = "";
        String connectionString = "Data Source=DESKTOP-QLJKDBL\\SQLEXPRESS;Initial Catalog=FinancialServicesCommisson;Integrated Security=True;";
        public void OnGet()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select * FROM OngoingUnassignedInvestigations";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                InvestInfo investInfo = new InvestInfo("referenceNum", "purpose", "investigationStartDate");

                                investInfo.referenceNum= reader.GetInt32(0).ToString();
                                investInfo.purpose = reader.GetString(1);
                                investInfo.investigationStartDate = reader.GetDateTime(2).ToString();
                                

                                //add ATM to list
                                unassignedList.Add(investInfo);
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

        public void OnPost()
        {
            try
            {
                //Assign Investigator stored procedure
                String auditorID = Request.Query["auditorID"];
                String referenceNum = Request.Form["referenceNum"];
                Console.WriteLine("Auditor ID: " + auditorID);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("AssignAuditorToInvestigation", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AuditorID", auditorID);
                        command.Parameters.AddWithValue("@InvestigationReferenceNum", referenceNum);
                        command.ExecuteNonQuery();
                    }
                }
                Response.Redirect("/Admin/AuditorPage");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
