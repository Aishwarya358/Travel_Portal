using System;
using System.Collections.Generic;

#nullable disable

namespace Entity_Model_Layer.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Bills = new HashSet<Bill>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Age { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
