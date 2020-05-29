using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookExchangerWebApi.Models.DTOs
{
    public class UserDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Points { get; set; }
        public int TotalUpload { get; set; }
        public int TotalBuy { get; set; }
        public int TotalSell { get; set; }
    }
}