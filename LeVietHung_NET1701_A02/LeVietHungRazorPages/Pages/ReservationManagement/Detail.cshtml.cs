using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace LeVietHungRazorPages.Pages.ReservationManagement
{
    public class DetailModel : PageModel
    {
        private readonly IBookingDetailRepository _bookingDetailRepository;
        public IList<BookingDetail> BookingReservations { get; set; } = default!;

        public DetailModel(IBookingDetailRepository bookingDetailRepository)
        {
            _bookingDetailRepository = bookingDetailRepository;
        }


        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingDetails = _bookingDetailRepository.GetBookDetailByBookingReservationID(id.ToString());
            if (bookingDetails == null)
            {
                return NotFound();
            }
            else
            {
                BookingReservations = bookingDetails;
            }
            return Page();
        }
    }
}
