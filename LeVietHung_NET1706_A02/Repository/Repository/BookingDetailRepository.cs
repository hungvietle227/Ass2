using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
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
