using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OakDevelopments.Data;
using OakDevelopments.Models;

namespace OakDevelopments.Pages.Properties
{
    public class AddModel : PageModel
    {
        private readonly AppDbContext _context;

        public AddModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Property ? Property { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || Property == null)
            {
                return Page();
            }

            _context.Properties.Add(Property);
            _context.SaveChanges();

            return RedirectToPage("/Properties/Index");
        }
    }
}