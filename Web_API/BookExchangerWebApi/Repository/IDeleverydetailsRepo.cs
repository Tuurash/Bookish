using BookExchangerWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchangerWebApi.Repository
{
    interface IDeleveryDetailsRepo : IRepository<DeleveryDetail>
    {
        int TotalDeliveries();

        IEnumerable<DeleveryDetail> GetDeliveryByID(int dmanID);
    }
}
