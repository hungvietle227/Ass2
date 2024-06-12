using BusinessObject.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BookingReservationRepository : IBookingReservationRepository
    {
        public BookingReservation CreateBookingReservation(BookingReservation bookingReservation)
        {
            return BookingReservationDAO.Instance.CreateBookingReservation(bookingReservation);
        }

        public IEnumerable<BookingReservation> GetAllBookingReservation()
        {
            return BookingReservationDAO.Instance.GetAllBookingReservation();
        }

        public IEnumerable<BookingReservation> GetAllBookingReservationByDate(DateTime startDate, DateTime endDate)
        {
            return BookingReservationDAO.Instance.GetAllBookingReservationByDate(startDate, endDate);
        }

        public List<BookingReservation>? GetBookingReservationByCustomerID(string id)
        {
            return BookingReservationDAO.Instance.GetBookingReservationByCustomerID(id);
        }

        public bool ReservationRoom(int reservationId)
        {
            return BookingReservationDAO.Instance.ReservationRoom(reservationId);
        }

        public List<BookingReservation> SearchBookingReservation(string searchValue)
        {
            return BookingReservationDAO.Instance.SearchBookingReservation(searchValue);
        }
    }
}
