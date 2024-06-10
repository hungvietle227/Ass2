using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IBookingReservationRepository
    {
        IEnumerable<BookingReservation> GetAllBookingReservation();
        IEnumerable<BookingReservation> GetAllBookingReservationByDate(DateTime startDate, DateTime endDate);
        List<BookingReservation>? GetBookingReservationByCustomerID(string id);
        List<BookingReservation> SearchBookingReservation(string searchValue);
    }
}
