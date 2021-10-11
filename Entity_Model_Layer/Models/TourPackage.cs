using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Entity_Model_Layer.Models
{
    public partial class TourPackage
    {
        public TourPackage()
        {
            Bills = new HashSet<Bill>();
            Bookings = new HashSet<Booking>();
            Bus = new HashSet<Bu>();
        }

        public int TourId { get; set; }
        [Required]
        [MaxLength(99, ErrorMessage = "Tour Name cannot Exceed 100 characters")]
        [MinLength(1)]

        public string TourName { get; set; }
        [Required]
        [MaxLength(99, ErrorMessage = "Source cannot Exceed 100 characters")]

        public string Source { get; set; }
        [Required]
        [MaxLength(99, ErrorMessage = "Destination cannot Exceed 100 characters")]
        [MinLength(1)]

        public string Destination { get; set; }
        public string Places { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Duration must be Numeric")]

        public int Duration { get; set; }
        [Required(ErrorMessage = "Fare is required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Fare must be Numeric")]

        public decimal Fare { get; set; }
        [Required]
        [MaxLength(499, ErrorMessage = "Cannot Exceed More than 500 characters")]

        public string Description { get; set; }
        [Required(ErrorMessage = "FoodAmount is required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "FoodAmount must be Numeric")]
        public decimal FoodAmount { get; set; }
        [Required(ErrorMessage = "StayAmount is required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "StayAmount must be Numeric")]
        public decimal StayAmount { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Bu> Bus { get; set; }
    }
}
