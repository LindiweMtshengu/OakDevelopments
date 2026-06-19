using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OakDevelopments.Data;
using OakDevelopments.Models;
using System.Reflection;

namespace OakDevelopments.Pages.Properties
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel 
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Property Property { get; set; }

        public List<Agent> Agents { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Property = await _context.Properties.FindAsync(id);
            Agents = _context.Agents.ToList();

            if (Property == null)
                return RedirectToPage("/Properties/Manage");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Agents = _context.Agents.ToList();

            if (!ModelState.IsValid)
                return Page();

            var existingProperty = await _context.Properties.FindAsync(Property.ID);

            if (existingProperty == null)
                return RedirectToPage("/Properties/Manage");

            existingProperty.Title = Property.Title;
            existingProperty.Description = Property.Description;
            existingProperty.Price = Property.Price;
            existingProperty.Bedrooms = Property.Bedrooms;
            existingProperty.Suburb = Property.Suburb;
            existingProperty.City = Property.City;
            existingProperty.Province = Property.Province;
            existingProperty.AgentID = Property.AgentID;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Properties/Manage");
        }
    }
}
