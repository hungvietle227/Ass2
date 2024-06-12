using BusinessObject.Models;
using LeVietHungRazorPages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Text.Json;

namespace LeVietHungRazorPages.Pages.ReservationManagement
{
    public class IndexModel : PageModel
    {
        private readonly IBookingReservationRepository _bookingReservationRepository;
        public Customer Customer { get; set; }
        public IList<BookingReservation> BookingReservations { get; set; } = default!;

        public IndexModel(IBookingReservationRepository bookingReservationRepository)
        {
            _bookingReservationRepository = bookingReservationRepository;
        }


        public IActionResult OnGet(int? id)
        {
            if (Request.Cookies.TryGetValue("Customer", out string customerJson))
            {
                Customer = JsonSerializer.Deserialize<Customer>(customerJson);
            }
            BookingReservations = _bookingReservationRepository.GetBookingReservationByCustomerID(Customer.CustomerId.ToString());
            if (id != null)
            {
                int roomId = (int)id;
                var result = _bookingReservationRepository.ReservationRoom(roomId);
                if (result)
                {
                    BookingReservations = _bookingReservationRepository.GetBookingReservationByCustomerID(Customer.CustomerId.ToString());
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
            return Page();
        }
    }
}
