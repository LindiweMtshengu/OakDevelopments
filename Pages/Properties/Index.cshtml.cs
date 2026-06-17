using Microsoft.AspNetCore.Mvc.RazorPages;
using OakDevelopments.Data;
using OakDevelopments.Models;
using System.Collections.Generic;
using System.Linq;

namespace OakDevelopments.Pages.Properties
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public List<Property> Properties { get; set; }

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Properties = _context.Properties.ToList();
        }
    }
}
