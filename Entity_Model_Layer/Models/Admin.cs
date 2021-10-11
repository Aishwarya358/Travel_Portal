using System;
using System.Collections.Generic;

#nullable disable

namespace Entity_Model_Layer.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
