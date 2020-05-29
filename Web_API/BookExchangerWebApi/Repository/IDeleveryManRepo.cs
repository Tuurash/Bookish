using BookExchangerWebApi.Models;
using BookExchangerWebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchangerWebApi.Repository
{
    interface IDeleveryManRepo : IRepository<DeleveryMan>
    {
        int TotalDeliveryMan();
        void AddDeliveryMan(DeleveryMan dm);
        IEnumerable<DeliveryManDTO> AllMen();
        void DeleteByEmail(string mail);
        bool IsEmailExist(string mail);
    }
}
