using ATMManagementApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ATMManagementApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(UserManager<ApplicationUser> userManager, ILogger<IndexModel> logger, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        

        public string BankName { get; set; }

        public async Task<IActionResult> OnGet()
        {
            // Retrieve the current user's ID
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                // Retrieve the user from the UserManager
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    // Check if the user is in the "Auditor" role
                    if (await _userManager.IsInRoleAsync(user, "Auditor"))
                    {
                        // If the user is an auditor, display their first name
                        BankName = "Welcome " + user.FirstName;
                    }
                    else
                    {
                        // Otherwise, display the bank name
                        BankName = "Welcome " + user.BankName;
                    }
                }
            }

            return Page();
        }
    }
}
