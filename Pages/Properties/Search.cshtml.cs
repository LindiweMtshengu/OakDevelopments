using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OakDevelopments.Data;
using OakDevelopments.Models;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OakDevelopments.Pages.Properties
{
    public class PropertiesSearchModel : PageModel
    {
        private readonly AppDbContext _context;

        public PropertiesSearchModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchType { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MinPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MaxPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Bedrooms { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Location { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? PropertyTypeID { get; set; }

        public List<Property> Results { get; set; } = new List<Property>();

        public void OnGet()
        {
            var query = _context.Properties.Include(p => p.Images).AsQueryable();

            if (PropertyTypeID.HasValue)
            {
                query = query.Where(p => p.PropertyTypeID == PropertyTypeID.Value);
            }

            if (MinPrice.HasValue)
                query = query.Where(p => p.Price >= MinPrice.Value);

            if (MaxPrice.HasValue)
                query = query.Where(p => p.Price <= MaxPrice.Value);

            if (Bedrooms.HasValue)
                query = query.Where(p => p.Bedrooms == Bedrooms.Value);

            if (!string.IsNullOrEmpty(Location))

                query = query.Where(p =>
                        (p.City != null && p.City.Contains(Location)) ||
                        (p.Suburb != null && p.Suburb.Contains(Location))
                    );


            Results = query.ToList();
        }
    }
}