using System;
using System.Collections.Generic;

#nullable disable

namespace Entity_Model_Layer.Models
{
    public partial class Bu
    {
        public int BusId { get; set; }
        public string BusName { get; set; }
        public int? TourId { get; set; }
        public string Ac { get; set; }
        public int TotalSeats { get; set; }
        public decimal Fare { get; set; }
        public string Status { get; set; }

        public virtual TourPackage Tour { get; set; }
    }
}
