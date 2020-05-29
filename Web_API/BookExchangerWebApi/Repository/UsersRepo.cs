using BookExchangerWebApi.Models;
using BookExchangerWebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookExchangerWebApi.Repository
{
    public class UsersRepo : Repository<User>, IUsersRepo
    {
        private readonly BookExchangerDBEntities context;

        public UsersRepo()
        {
            context = new BookExchangerDBEntities();
        }

        public IEnumerable<UserDTO> AllUsers()
        {
            var users = context.Users.Select(p => new UserDTO()
            {
                FullName = p.FullName,
                Email = p.Email,
                Points = p.Points,
                TotalUpload = context.Books.Count(x => x.UserId == p.UserId),
                TotalBuy = context.SellPostings.Count(x => x.BuyerId == p.UserId && x.Status == 4),
                TotalSell = context.SellPostings.Count(x => x.SellerId == p.UserId && x.Status == 4)
            });

            return users.ToList();
        }

        public void DeleteByEmail(string mail)
        {
            var li = context.Logins.Single(x => x.Email == mail && x.Status == 0);
            var us = context.Users.Single(x => x.Email == mail);

            context.Logins.Remove(li);
            context.Users.Remove(us);

            context.SaveChanges();
        }

        public int TotalUsers()
        {
            return context.Users.Count();
        }
        public User GetUserByEmail(string mail)
        {
            var us = context.Users.FirstOrDefault(x => x.Email == mail);
            return us;
        }

        public int GetStatus(string mail)
        {
            Login l;
            l = context.Logins.FirstOrDefault(x=>x.Email==mail);

            return l.Status;
        }

        public int GetPoint(int id)
        {
            User u;
            u = context.Users.FirstOrDefault(x=>x.UserId==id);
            return u.Points;
        }

        public int PutPoint(int id, int point)
        {
            var b = context.Users.SingleOrDefault(x => x.UserId == id);

            if (b != null)
            {
                b.Points = point;
                context.SaveChanges();
                return b.Points;
            }
            else
            {
                return 0;
            }
        }
    }
}