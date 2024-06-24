using BusinessObject.Models;
using DataAccess.SignalRHub;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Repository;
using LeVietHungRazorPages.Helper;

namespace LeVietHungRazorPages.Pages.Admin
{
    public class ViewBookingModel : PageModel
    {
        private readonly IBookingReservationRepository _bookingRepository;

        public ViewBookingModel(IBookingReservationRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public IList<BookingReservation> BookingInformation { get; set; } = default!;

        public void OnGet()
        {
            BookingInformation = _bookingRepository.GetAllBookingReservation().ToList();
        }
    }
}
