using BusinessObject.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        public BookingDetail CreateBookingDetail(BookingDetail bookingReservation)
        {
            return BookingDetailDAO.Instance.CreateBookingDetail(bookingReservation);
        }

        public List<BookingDetail>? GetBookDetailByBookingReservationID(string id)
        {
            return BookingDetailDAO.Instance.GetBookDetailByBookingReservationID(id);
        }

        public List<BookingDetail> SearchBookingDetail(string searchValue)
        {
            return BookingDetailDAO.Instance.SearchBookingDetail(searchValue);
        }
    }
}
