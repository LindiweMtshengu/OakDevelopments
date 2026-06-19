using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OakDevelopments.Data;
using OakDevelopments.Models;
using System.Linq;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;

    public DetailsModel(AppDbContext context)
    {
        _context = context;
    }

    public Property ? Property { get; set; }

    public void OnGet(int id)
    {
        Property = _context.Properties
            .Include(p => p.Images)
            .Include(p => p.Agent)
            .FirstOrDefault(p => p.ID == id);
    }
}
