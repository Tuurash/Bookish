using BookExchangerWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookExchangerWebApi.Repository
{
    public class DeleveryDetailsRepo : Repository<DeleveryDetail>, IDeleveryDetailsRepo
    {
        private readonly BookExchangerDBEntities context;

        public DeleveryDetailsRepo()
        {
            context = new BookExchangerDBEntities();
        }

        public int TotalDeliveries()
        {
            return context.SellPostings.Count(x => x.Status == 4);
        }

        #region Turash
        public IEnumerable<DeleveryDetail> GetDeliveryByID(int dmanID)
        {
            return context.DeleveryDetails.Where(x => x.DeleveryManId == dmanID).ToList();
        }


        #endregion
    }
}