using FribergCarRentals.Interfaces;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IAuthService _auth;

        public DeleteModel(IBookingRepository bookingRepo, IAuthService auth)
        {
            _bookingRepo = bookingRepo;
            _auth = auth;
            Object = new DeleteVM();
        }

        [BindProperty]
        public DeleteVM Object { get; set; }
       

        public IActionResult OnGetBooking(int id)
        {
            var result = _auth.CheckCustomerAuth();
            if (!result.Success)
            {
                TempData["expired"] = result.Message;
                return RedirectToPage("Login");
            }

            Object.Booking = _bookingRepo.GetById(id);
            return Page();
        }

        public IActionResult OnPostBooking()        // TODO: Fix auth onPost?
        {
            var result = _auth.CheckCustomerAuth();
            if (!result.Success)
            {
                TempData["expired"] = result.Message;
                return RedirectToPage("Login");
            }

            _bookingRepo.Delete(Object.Booking.BookingId);

            return RedirectToPage("List", "Bookings");
        }
    }
}
