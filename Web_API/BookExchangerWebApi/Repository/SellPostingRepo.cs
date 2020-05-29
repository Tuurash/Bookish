using BookExchangerWebApi.Models;
using BookExchangerWebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookExchangerWebApi.Repository
{
    public class SellPostingRepo : Repository<SellPosting>, ISellPostingRepo
    {
        private readonly BookExchangerDBEntities context;

        public SellPostingRepo()
        {
            context = new BookExchangerDBEntities();
        }

        public IEnumerable<DeliveryStatusDTO> DeliveryStatus()
        {
            var records = context.SellPostings.OrderByDescending(x => x.SellId).Select(x => new DeliveryStatusDTO()
            {
                SellID = x.SellId,
                BookName = context.Books.Where(y => y.BookId == x.BookId).Select(y => y.BookTitle).FirstOrDefault(),
                SellerName = context.Users.Where(z => z.UserId == x.SellerId).Select(z => z.FullName).FirstOrDefault(),
                BuyerName = context.Users.Where(z => z.UserId == x.BuyerId).Select(z => z.FullName).FirstOrDefault(),
                Status = x.Status
            }) ;

            return records.ToList();
        }

        public StatusCountDTO StatusCount()
        {
            int accepted = context.SellPostings.Count(x => x.Status == 1);
            int way = context.SellPostings.Count(x => x.Status == 3);
            int delivered = context.SellPostings.Count(x => x.Status == 4);

            return new StatusCountDTO { Accepted = accepted, OnWay = way, Delivered = delivered };
        }
   
        #region Turash

        public SellPosting GetbyUserId(int userId)
        {
            return context.SellPostings.FirstOrDefault(x => x.BuyerId == userId && x.Status == 0);
        }

        public IEnumerable<DeliveryStatusDTO> PendingList()
        {
            var records = context.SellPostings.Where(x => x.Status == 1).Select(x => new DeliveryStatusDTO()
            {
                SellID = x.SellId,
                BookName = context.Books.Where(y => y.BookId == x.BookId).Select(y => y.BookTitle).FirstOrDefault(),
                SellerName = context.Users.Where(z => z.UserId == x.SellerId).Select(z => z.FullName).FirstOrDefault(),
                BuyerName = context.Users.Where(z => z.UserId == x.BuyerId).Select(z => z.FullName).FirstOrDefault(),
                Status = x.Status
            });

            return records.ToList();
        }
        #endregion

    }
}