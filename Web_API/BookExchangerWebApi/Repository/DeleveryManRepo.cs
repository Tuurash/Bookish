using BookExchangerWebApi.Models;
using BookExchangerWebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace BookExchangerWebApi.Repository
{
    public class DeleveryManRepo : Repository<DeleveryMan>, IDeleveryManRepo
    {
        private readonly BookExchangerDBEntities context;
        private readonly Random rand;

        public DeleveryManRepo()
        {
            context = new BookExchangerDBEntities();
            rand = new Random();
        }

        public void AddDeliveryMan(DeleveryMan dm)
        {
            int password = rand.Next(1111, 9999);

            try
            {
                using (MailMessage mm = new MailMessage("turash1155049@gmail.com", dm.Email))
                {
                    mm.Subject = "Book Exchange Login";
                    mm.Body = "Email: " + dm.Email + Environment.NewLine + "Password: " + password;

                    mm.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential credential = new NetworkCredential("turash1155049@gmail.com", "turash1234");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = credential;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }
            }
            catch (Exception) { }
            finally
            {
                Login li = new Login { Email = dm.Email, Password = Convert.ToString(password), Status = 2 };

                context.DeleveryMen.Add(dm);
                context.Logins.Add(li);
                context.SaveChanges();
            }
        }

        public IEnumerable<DeliveryManDTO> AllMen()
        {
            var records = context.DeleveryMen.OrderByDescending(a => a.DeleveryManId).Select(x => new DeliveryManDTO()
            {
                FullName = x.FullName,
                Email = x.Email,
                Phone = x.Phone,
                Count = context.DeleveryDetails.Count(y => y.DeleveryManId == x.DeleveryManId)
            });

            return records.ToList();
        }

        public int TotalDeliveryMan()
        {
            return context.DeleveryMen.Count();
        }

        public void DeleteByEmail(string mail)
        {
            var li = context.Logins.Single(x => x.Email == mail && x.Status == 2);
            var us = context.DeleveryMen.Single(x => x.Email == mail);

            context.Logins.Remove(li);
            context.DeleveryMen.Remove(us);

            context.SaveChanges();
        }

        public bool IsEmailExist(string mail)
        {
            DeleveryMan record = null;
            User u = null;

            if (context.DeleveryMen.Any())
                record = context.DeleveryMen.FirstOrDefault(x => x.Email == mail);

            if (context.Users.Any())
                u = context.Users.FirstOrDefault(x => x.Email == mail);

            if (record != null || u != null)
                return true;

            return false;
        }
    }
}