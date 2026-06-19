using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OakDevelopments.Data;
using OakDevelopments.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

[Authorize(Roles = "Admin")]
public class BookingsModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly IEmailSender _emailSender;

    [BindProperty]
    public int BookingId { get; set; }

    [BindProperty]
    public string Comment { get; set; }

    [BindProperty]
    public string Action { get; set; }

    public BookingsModel(AppDbContext context, IEmailSender emailSender)
    {
        _context = context;
        _emailSender = emailSender;
    }

    public List<Booking> Bookings { get; set; }



    public async Task OnGetAsync()
    {
        Bookings = await _context.Bookings
            .Include(b => b.Property)
            .Where(b => b.Status == "Pending")
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var booking = await _context.Bookings
            .Include(b => b.Property)
            .FirstOrDefaultAsync(b => b.ID == BookingId);

        if (booking == null)
            return NotFound();

        booking.Status = Action;
        booking.Comment = Comment ?? "";

        await _context.SaveChangesAsync();

        // BUILD EMAIL MESSAGE
        string message = $@"
                        <div style='font-family:Arial; line-height:1.6;'>

                            <h2 style='color:#d4af37;'>Booking Update</h2>

                            <p>Dear {booking.Name},</p>

                            <p>
                                Your viewing request has been <strong>{booking.Status}</strong>.
                            </p>

                            <hr style='margin:20px 0;' />

                            <h4>Property Details</h4>
                            <p>
                                <strong>Property:</strong> {booking.Property.Title}<br/>
                                <strong>Location:</strong> {booking.Property.Suburb}, {booking.Property.City}<br/>
                            </p>

                            <h4>Booking Details</h4>
                            <p>
                                <strong>Requested Date:</strong> {booking.RequestedDate:yyyy-MM-dd HH:mm}<br/>
                                <strong>Status:</strong> {booking.Status}
                            </p>

                            <h4>Admin Message</h4>
                            <p>
                                {booking.Comment}
                            </p>

                            <hr style='margin:20px 0;' />

                            <p>
                                If you have any questions, feel free to reply to this email.
                            </p>

                            <p>
                                Regards,<br/>
                                <strong>OakDevelopments Team</strong>
                            </p>

                        </div>
                        ";

        // SEND EMAIL
        await _emailSender.SendEmailAsync(
            booking.Email,
            "Your Viewing Request Update",
            message
        );

        TempData["Success"] = $"Booking {Action.ToLower()} and email sent ";

        return RedirectToPage();
    }
}