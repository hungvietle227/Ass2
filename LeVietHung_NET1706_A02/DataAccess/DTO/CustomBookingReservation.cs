using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class CustomBookingReservation
    {
        public int BookingReservationId { get; set; }

        public DateOnly? BookingDate { get; set; }

        public decimal? TotalPrice { get; set; }

        public int CustomerId { get; set; }

        public string? BookingStatus { get; set; }

    }
}
