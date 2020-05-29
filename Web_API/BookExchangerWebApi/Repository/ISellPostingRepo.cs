using BookExchangerWebApi.Models;
using BookExchangerWebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchangerWebApi.Repository
{
    interface ISellPostingRepo : IRepository<SellPosting>
    {
        SellPosting GetbyUserId(int userId);

        IEnumerable<DeliveryStatusDTO> DeliveryStatus();

        StatusCountDTO StatusCount();

        IEnumerable<DeliveryStatusDTO> PendingList();

    }
}
