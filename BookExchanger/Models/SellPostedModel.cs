using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookExchanger.Models
{
    public class SellPostedModel
    {
      
            public int id { get; set; }
            public int BookId { get; set; }
            public int offeredPoint { get; set; }
            public int BuyerId { get; set; }
            public int SellerId { get; set; }
            public int Status { get; set; }
    
    }
}