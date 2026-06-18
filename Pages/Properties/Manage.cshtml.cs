using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OakDevelopments.Data;
using OakDevelopments.Models;

namespace OakDevelopments.Pages.Properties
{
    [Authorize(Roles = "Admin")]
    public class ManageModel : PageModel
    {
        private readonly AppDbContext _context;

        public ManageModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Property> Properties { get; set; }

        public async Task OnGetAsync()
        {
            Properties = await _context.Properties
                .Include(p => p.Images)
                .ToListAsync();
        }

        // DELETE PROPERTY
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var property = await _context.Properties
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (property == null)
                return RedirectToPage();

            // delete images from disk
            foreach (var img in property.Images)
            {
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    img.ImagePath.TrimStart('/')
                );

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            // delete from DB
            _context.PropertyImages.RemoveRange(property.Images);
            _context.Properties.Remove(property);

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
