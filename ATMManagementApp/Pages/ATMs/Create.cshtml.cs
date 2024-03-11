using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ATMManagementApp.Pages.ATMs
{
    public class CreateModel : PageModel
    {
        public IndexModel.ATMInfo atmInfo =new IndexModel.ATMInfo("FINumber", "atmName", "atmAddress", "atmType");
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            atmInfo.FINumber = Request.Form["FINumber"];
            atmInfo.atmName = Request.Form["atmName"];
            atmInfo.atmAddress = Request.Form["atmAddress"];
            atmInfo.atmType = Request.Form["atmType"];

            if(atmInfo.atmName.Length ==0 ||  atmInfo.atmAddress.Length == 0 || 
                atmInfo.FINumber.Length==0 || atmInfo.atmType.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the atm to the database
            try
            {
                String connectionString = "Data Source=DESKTOP-QLJKDBL\\SQLEXPRESS;Initial Catalog=FinancialServicesCommisson;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO ATM (FINumber, atmName, atmAddress, atmType) VALUES (@FINumber, @atmName, @atmAddress, @atmType);";

                    //Get info from form and pass it to variables
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Convert FINumber to int before adding it as a parameter
                        int fiNumber;
                        if (int.TryParse(atmInfo.FINumber, out fiNumber))
                        {
                            command.Parameters.AddWithValue("@FINumber", fiNumber);
                            command.Parameters.AddWithValue("@atmName", atmInfo.atmName);
                            command.Parameters.AddWithValue("@atmAddress", atmInfo.atmAddress);
                            command.Parameters.AddWithValue("@atmType", atmInfo.atmType);

                        }

                        command.ExecuteNonQuery();
                        Console.WriteLine($"FINumber: {atmInfo.FINumber}, atmName: {atmInfo.atmName}, atmAddress: {atmInfo.atmAddress}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid FINumber: " + atmInfo.FINumber);
                errorMessage = ex.Message;
                return;
            }

            atmInfo.FINumber = ""; atmInfo.atmName = ""; atmInfo.atmAddress = ""; atmInfo.atmType = "";

            successMessage = "New ATM added successfully";

            //Redirects user
            Response.Redirect("/ATMs/Index");
        }
    }
}
