using System;
using System.Collections.Generic;

#nullable disable

namespace Entity_Model_Layer.Models
{
    public partial class Booking
    {
        public int BookingId { get; set; }
        public int? TourId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int TotalPassengers { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }

        public virtual TourPackage Tour { get; set; }
    }
}
