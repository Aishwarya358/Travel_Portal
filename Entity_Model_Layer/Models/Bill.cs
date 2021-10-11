using System;
using System.Collections.Generic;

#nullable disable

namespace Entity_Model_Layer.Models
{
    public partial class Bill
    {
        public int BillId { get; set; }
        public int? CustomerId { get; set; }
        public int? TourId { get; set; }
        public string CustomerName { get; set; }
        public string PlaceName { get; set; }
        public int Passengers { get; set; }
        public decimal StayCost { get; set; }
        public decimal FoodCost { get; set; }
        public decimal TravelingCost { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual TourPackage Tour { get; set; }
    }
}
