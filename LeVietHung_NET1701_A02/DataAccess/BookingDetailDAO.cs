using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BookingDetailDAO
    {
        private static BookingDetailDAO? instance;
        private static readonly object instanceLook = new object();
        public BookingDetailDAO() { }
        public static BookingDetailDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new BookingDetailDAO();
                    }
                }
                return instance;
            }
        }
        public List<BookingDetail>? GetBookDetailByBookingReservationID(string id)
        {
            int.TryParse(id, out var ID);
            using var db = new FuminiHotelManagementContext();
            List<BookingDetail>? bookingDetail = db.BookingDetails.Include(a=>a.Room).Where(c => c.BookingReservationId == ID).ToList();
            return bookingDetail;
        }

        public List<BookingDetail> SearchBookingDetail(string searchValue)
        {
            var listBookingDetail = new List<BookingDetail>();
            try
            {
                using var db = new FuminiHotelManagementContext();
                listBookingDetail = db.BookingDetails.Where(a => a.RoomId.ToString() == searchValue || a.ActualPrice.ToString() == searchValue).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listBookingDetail;
        }

        public BookingDetail CreateBookingDetail(BookingDetail bookingReservation)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                var item = db.BookingDetails.Add(bookingReservation).Entity;
                var result = db.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
