using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OakDevelopments.Data;
using OakDevelopments.Models;

namespace OakDevelopments.Pages.Properties
{
    [Authorize(Roles = "Admin")]
    public class AddModel : PageModel
    {
        private readonly AppDbContext _context;

        public AddModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Property Property { get; set; } = new Property();

        public List<Agent> Agents { get; set; }

        [BindProperty]
        public List<IFormFile> Images { get; set; }

        public SelectList StatusList { get; set; }

        public void OnGet()
        {
            Agents = _context.Agents.ToList();
            StatusList = new SelectList(_context.PropertyStatuses, "ID", "StatusName");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Agents = _context.Agents.ToList();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // AUTO ASSIGN REQUIRED FIELDS 
            //Property.AgentID = 1;
            //Property.PropertyTypeID = 1;
            //Property.StatusID = 1;

            // Validate images
            if (Images == null || Images.Count < 4)
            {
                ModelState.AddModelError("", "Please upload at least 4 images.");
                return Page();
            }

            // Save property
            _context.Properties.Add(Property);
            await _context.SaveChangesAsync();

            // Save images
            foreach (var file in Images)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/images",
                    fileName
                );

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var image = new PropertyImage
                {
                    ImagePath = "/images/" + fileName,
                    PropertyID = Property.ID
                };

                _context.PropertyImages.Add(image);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("/Properties/Index");
        }
    }
}