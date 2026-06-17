using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OakDevelopments.Data;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();


// Add Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ADD THESE (CRITICAL FOR LOGIN)
app.UseAuthentication();  
app.UseAuthorization();    

app.MapRazorPages();

app.Run();
