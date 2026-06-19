using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Authorize(Roles = "Admin")]
public class ManageUsersModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;

    public ManageUsersModel(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public List<UserViewModel> Users { get; set; }

    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }

    public async Task OnGetAsync()
    {
        var users = _userManager.Users.ToList();

        Users = new List<UserViewModel>();

        foreach (var user in users)
        {
            Users.Add(new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                IsAdmin = await _userManager.IsInRoleAsync(user, "Admin")
            });
        }
    }

    public async Task<IActionResult> OnPostMakeAdminAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user != null)
        {
            await _userManager.AddToRoleAsync(user, "Admin");
            TempData["Success"] = "User promoted to Admin!";
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostRemoveAdminAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user != null)
        {
            await _userManager.RemoveFromRoleAsync(user, "Admin");
            TempData["Success"] = "User removed as Admin!";
        }

        return RedirectToPage();
    }

}