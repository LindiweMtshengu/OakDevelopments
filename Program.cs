using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using OakDevelopments.Data;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>();


// Add Razor Pages
builder.Services.AddRazorPages();

builder.Services.AddTransient<IEmailSender, EmailSender>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    string adminRole = "Admin";
    string adminEmail = "admin@oak.com";
    string adminPassword = "Admin@123";

    // Create Admin role
    if (!await roleManager.RoleExistsAsync(adminRole))
    {
        await roleManager.CreateAsync(new IdentityRole(adminRole));
    }

    // Create Admin user
    var user = await userManager.FindByEmailAsync(adminEmail);
    if (user == null)
    {
        user = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true // avoid login issues
        };

        await userManager.CreateAsync(user, adminPassword);
    }

    // Assign Admin role
    if (!await userManager.IsInRoleAsync(user, adminRole))
    {
        await userManager.AddToRoleAsync(user, adminRole);
    }
}



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
