using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookExchangerWebApi.Models.DTOs
{
    public class StatusCountDTO
    {
        public int Accepted { get; set; }
        public int OnWay { get; set; }
        public int Delivered { get; set; }
    }
}