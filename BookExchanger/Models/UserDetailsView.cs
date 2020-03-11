using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookExchanger.Models
{
    public class UserDetailsView
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public int Phone { get; set; }

        public string Address { get; set; }

        public int Point { get; set; }
        
    }
}