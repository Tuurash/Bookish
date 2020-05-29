using BookExchangerWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookExchangerWebApi.Attributes
{
    public class Security
    {
        public static int Login(string userEmail, string password)
        {
            using (BookExchangerDBEntities entities = new BookExchangerDBEntities())
            {
                Login l= entities.Logins.FirstOrDefault(x => x.Email == userEmail && x.Password == password);
                return l.Status;
            }
        }
    }
}