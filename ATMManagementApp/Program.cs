using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ATMManagementApp.Services;
using ATMManagementApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("ATMManagementAppDbContextConnection");
    options.UseSqlServer(connectionString);
});

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDBContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();