using Microsoft.AspNetCore.Authorization;
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

    [BindProperty]
    public decimal MonthlyPayment
    {
        get; set;
    }

    public void OnGet(int id)
    {
        Property = _context.Properties
            .Include(p => p.Images)
            .Include(p => p.Agent)
            .Include(p => p.Status)
            .FirstOrDefault(p => p.ID == id);
    }


    public async Task<IActionResult> OnPostBookViewingAsync(int propertyId)
    {
        if (!User.Identity.IsAuthenticated)
        {
            TempData["Error"] = "Please log in to book a viewing.";

            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        // User is logged in ? go to booking page
        return RedirectToPage("/Bookings/Create", new { propertyId = propertyId });
    }

    public IActionResult OnPostCalculateMortgage(decimal price, decimal deposit, double interestRate, int years)
    {
        double loanAmount = (double)(price - deposit);

        double monthlyRate = (interestRate / 100) / 12;
        double numberOfPayments = years * 12;

        if (monthlyRate > 0)
        {
            MonthlyPayment = (decimal)(
                loanAmount * monthlyRate /
                (1 - Math.Pow(1 + monthlyRate, -numberOfPayments))
            );
        }

        TempData["MortgageResult"] = $"Estimated Monthly Payment: R {MonthlyPayment:F2}";

        return RedirectToPage(new { id = RouteData.Values["id"] });
    }



}
