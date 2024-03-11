using Microsoft.AspNetCore.Identity;

namespace ATMManagementApp.Models
{
    public class ApplicationUser: IdentityUser
    {

        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Address { get; set; } = "";

        public DateTime CreatedAt { get; set; }

        //New columns
        public int FINumber { get; set; }
        public string BankName { get; set; } = "";
        public string parish { get; set; } = "";
        public int BranchNumber { get; set; }
       
    }

}

