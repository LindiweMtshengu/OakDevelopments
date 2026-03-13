using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OakDevelopments.Data;
using OakDevelopments.Models;
using System.Collections.Generic;
using System.Linq;

namespace OakDevelopments.Pages.Agents
{
    public class AgentsSearchModel : PageModel
    {
        private readonly AppDbContext _context;

        public AgentsSearchModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string Location { get; set; }

        public List<Agent> Results { get; set; } = new List<Agent>();

        public void OnGet()
        {
            var query = _context.Agents.AsQueryable();

            if (!string.IsNullOrEmpty(Location))
            {
                query = query.Where(a => a.City.Contains(Location) || a.Suburb.Contains(Location));
            }

            Results = query.ToList();
        }
    }
}