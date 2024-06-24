using BusinessObject.Models;
using DataAccess;
using DataAccess.SignalRHub;
using LeVietHungRazorPages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Text.Json;

namespace LeVietHungRazorPages.Pages.ReservationManagement
{
    public class IndexModel : PageModel
    {
        private readonly IBookingReservationRepository _bookingReservationRepository;
        private readonly IHubContext<RoomHub> _hubContext;

        public Customer Customer { get; set; }
        public IList<BookingReservation> BookingReservations { get; set; } = default!;

        public IndexModel(IBookingReservationRepository bookingReservationRepository, IHubContext<RoomHub> hubContext)
        {
            _bookingReservationRepository = bookingReservationRepository;
            _hubContext = hubContext;
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
                var idRoom = BookingReservationDAO.Instance.GetRoomIdByReservationId((int)id);
                foreach (var item in idRoom)
                {
                    SendRoomUpdate(item, 1);
                };
                var result = _bookingReservationRepository.ReservationRoom(roomId);
                if (result)
                {
                    BookingReservations = _bookingReservationRepository.GetBookingReservationByCustomerID(Customer.CustomerId.ToString());

                    return RedirectToPage("/RoomManagement/Index");
                }
                else
                {
                    return NotFound();
                }
            }
            return Page();
        }

        private void SendRoomUpdate(int roomId, int status)
        {
            // Tạo một task chạy đồng bộ
            var task = _hubContext.Clients.All.SendAsync("ReceiveRoomUpdate", roomId, status);
            task.Wait();
        }
    }
}
