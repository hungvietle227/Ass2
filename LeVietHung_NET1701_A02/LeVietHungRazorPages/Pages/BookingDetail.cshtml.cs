using BusinessObject.Models;
using DataAccess.SignalRHub;
using LeVietHungRazorPages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Repository;
using System.Text.Json;

namespace LeVietHungRazorPages.Pages
{
    public class BookingDetailModel : PageModel
    {
        private readonly IBookingReservationRepository _bookingReservationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingDetailRepository _bookingDetailRepository;
        private readonly IHubContext<RoomHub> _hubContext;

        //[BindProperty(SupportsGet = true)]
        //public List<int> SelectedRoomIds { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedRoomIds { get; set; }

        [BindProperty]
        public DateTime StartDate { get; set; }

        [BindProperty]
        public DateTime EndDate { get; set; }

        public Customer Customer { get; set; }

        public List<int> RoomIds { get; private set; }


        public BookingDetailModel(IBookingReservationRepository bookingReservationRepository, IRoomRepository roomRepository,
            IBookingDetailRepository bookingDetailRepository, IHubContext<RoomHub> hubContext)
        {
            _bookingReservationRepository = bookingReservationRepository;
            _roomRepository = roomRepository;
            _bookingDetailRepository = bookingDetailRepository;
            _hubContext = hubContext;
        }

        public IActionResult OnGet()
        {
            DateTime today = DateTime.Today;

            StartDate = today;
            EndDate = today;

            Customer = SessionHelper.GetObjectFromJson<Customer>(HttpContext.Session, "Customer");
            // Kiểm tra xem có RoomId nào được truyền qua không
            if (string.IsNullOrEmpty(SelectedRoomIds))
            {
                // Nếu không có RoomId nào được truyền, có thể thực hiện một xử lý phù hợp ở đây
                return RedirectToPage("/RoomManagement/Index");
            }

            RoomIds = SelectedRoomIds.Split(',').Select(int.Parse).ToList();

            // Xử lý các RoomId đã chọn, ví dụ: lưu vào cơ sở dữ liệu
            foreach (var roomId in RoomIds)
            {
                Console.WriteLine(roomId);
                // Gửi thông báo cập nhật trạng thái phòng đồng bộ
                SendRoomUpdate(roomId, 0);
                // Xử lý RoomId
            }
            return Page();
        }

        public IActionResult OnPost(DateTime startDate, DateTime endDate, string otherFields)
        {
            if (Request.Cookies.TryGetValue("Customer", out string customerJson))
            {
                // Deserialize thông tin khách hàng từ chuỗi JSON
                Customer = JsonSerializer.Deserialize<Customer>(customerJson);
            }
                // la phai tao cai bang booking reservation truoc va sau do moi tao bang booking detail
                //Customer = SessionHelper.GetObjectFromJson<Customer>(HttpContext.Session, "Customer");
                //var id = Customer.CustomerId;
                var listIdRoom = SelectedRoomIds.Split(',').Select(int.Parse).ToList();
            _roomRepository.UpdateStatusRoom(listIdRoom);
            Random random = new Random();
            int randomId = random.Next(1, 999999999);
            decimal totalPrice = (decimal)_roomRepository.GetTotalPriceByListRoomId(listIdRoom);
            BookingReservation bookingReservation = new BookingReservation();
            bookingReservation.BookingReservationId = randomId;
            bookingReservation.CustomerId = Customer.CustomerId;
            bookingReservation.TotalPrice = totalPrice;
            bookingReservation.BookingDate = DateOnly.FromDateTime(startDate);
            bookingReservation.BookingStatus = 0;
            var result = _bookingReservationRepository.CreateBookingReservation(bookingReservation);
            foreach (var roomId in listIdRoom)
            {
                BookingDetail bookingDetail = new BookingDetail();
                bookingDetail.RoomId = roomId;
                bookingDetail.BookingReservationId = result.BookingReservationId;
                bookingDetail.StartDate = DateOnly.FromDateTime(startDate);
                bookingDetail.EndDate = DateOnly.FromDateTime(endDate);
                var priceByRoom = _roomRepository.GetRoomInfoByID(roomId.ToString()).RoomPricePerDay;
                bookingDetail.ActualPrice = priceByRoom;
                var result2 = _bookingDetailRepository.CreateBookingDetail(bookingDetail);
            }

            return RedirectToPage("/ReservationManagement/Index");
            // Xử lý lưu thông tin đặt phòng vào cơ sở dữ liệu ở đây
            // Sử dụng startDate, endDate và các trường dữ liệu khác được gửi từ form
        }

        public IActionResult OnPostBackToRoom()
        {
            // Lấy danh sách phòng đã chọn từ query string
            var roomIds = SelectedRoomIds.Split(',').Select(int.Parse).ToList();

            // Gửi thông báo cập nhật trạng thái phòng thông qua SignalR
            foreach (var roomId in roomIds)
            {
                // Gửi thông báo cập nhật trạng thái phòng đồng bộ
                SendRoomUpdate(roomId, 1);
                // Xử lý RoomId
            }

            // Chuyển hướng về trang quản lý phòng
            return RedirectToPage("/RoomManagement/Index");
        }

        private void SendRoomUpdate(int roomId, int status)
        {
            // Tạo một task chạy đồng bộ
            var task = _hubContext.Clients.All.SendAsync("ReceiveRoomUpdate", roomId, status);
            task.Wait();
        }
    }
}
