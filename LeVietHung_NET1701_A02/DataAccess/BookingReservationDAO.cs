using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BookingReservationDAO
    {
        private static BookingReservationDAO? instance;
        private static readonly object instanceLook = new object();
        public BookingReservationDAO() { }
        public static BookingReservationDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new BookingReservationDAO();
                    }
                }
                return instance;
            }
        }
        public IEnumerable<BookingReservation> GetAllBookingReservation()
        {
            var listBookingReservation = new List<BookingReservation>();
            try
            {
                using var db = new FuminiHotelManagementContext();
                listBookingReservation = db.BookingReservations.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listBookingReservation;
        }

        public IEnumerable<BookingReservation> GetAllBookingReservationByDate(DateTime startDate, DateTime endDate)
        {
            var listBookingReservation = new List<BookingReservation>();
            try
            {
                using var db = new FuminiHotelManagementContext();
                listBookingReservation = db.BookingReservations.Where(a => a.BookingDate >= ToDateOnly(startDate) && a.BookingDate <= ToDateOnly(endDate)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listBookingReservation;
        }

        public DateOnly ToDateOnly(DateTime dateTime)
        {
            return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        public List<BookingReservation>? GetBookingReservationByCustomerID(string id)
        {
            List<BookingReservation> listBookingReservation = new List<BookingReservation> ();
            int.TryParse(id, out var ID);
            using var db = new FuminiHotelManagementContext();
            listBookingReservation = db.BookingReservations.Where(c => c.CustomerId == ID).ToList();
            return listBookingReservation;
        }

        public List<BookingReservation> SearchBookingReservation(string searchValue)
        {
            var listBookingReservation = new List<BookingReservation>();
            try
            {
                using var db = new FuminiHotelManagementContext();
                listBookingReservation = db.BookingReservations.Where(a => a.TotalPrice.ToString().Contains(searchValue)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listBookingReservation;
        }
        public BookingReservation CreateBookingReservation(BookingReservation bookingReservation)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                var item = db.BookingReservations.Add(bookingReservation).Entity;
                var result = db.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ReservationRoom(int reservationId)
        {
            using var db = new FuminiHotelManagementContext();
            var reservation = db.BookingReservations.Where(a => a.BookingReservationId == reservationId).FirstOrDefault();
            reservation.BookingStatus = 1;
            db.BookingReservations.Update(reservation);
            db.SaveChanges();
            var listRoomId = db.BookingDetails.Where(a => a.BookingReservationId == reservationId).ToList();
            var existedRoomId = db.RoomInformations.ToList();
            if (listRoomId.Count > 0)
            {
                var commonRoomIds = listRoomId.Select(r => r.RoomId).Intersect(existedRoomId.Select(r => r.RoomId)).ToList();
                foreach (var room in commonRoomIds)
                {
                    var item = db.RoomInformations.Where(a => a.RoomId == room).FirstOrDefault();
                    item.RoomStatus = 1;
                    db.RoomInformations.Update(item);
                    db.SaveChanges();
                }
                return true;
            }
            return false;
        }
    }
}
