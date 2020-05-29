using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookExchangerWebApi.Models.DTOs
{
    public class DeliveryStatusDTO
    {
        public int SellID { get; set; }
        public string BookName { get; set; }
        public string SellerName { get; set; }
        public string BuyerName { get; set; }
        public int Status { get; set; }
    }
}