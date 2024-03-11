using ATMManagementApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ATMManagementApp.Services
{
    public class ApplicationDBContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext()
        {
        }

        public ApplicationDBContext(DbContextOptions options): base(options) 
        { 
        }

        public object InvestigationReport { get; internal set; }



        //Creating roles
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var fi = new IdentityRole("fi");
            fi.NormalizedName = "fi";

            var auditor = new IdentityRole("auditor");
            auditor.NormalizedName = "auditor";

            builder.Entity<IdentityRole>().HasData(admin, fi, auditor);
        }
    }
}
