using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OakDevelopments.Data;
using OakDevelopments.Models;

namespace OakDevelopments.Pages.Bookings
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Booking Booking { get; set; }

        public void OnGet(int propertyId)
        {
            Booking = new Booking
            {
                PropertyId = propertyId,
                RequestedDate = DateTime.Now
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Booking.Status = "Pending";

            if (string.IsNullOrEmpty(Booking.Comment))
            {
                Booking.Comment = "";
            }


            _context.Bookings.Add(Booking);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Booking submitted successfully!";

            return RedirectToPage("/Index");
        }
    }
}
