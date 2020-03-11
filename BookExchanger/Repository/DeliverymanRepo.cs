using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookExchanger.Models;
using BookExchanger.Models.UsersModel;


namespace BookExchanger.Repository
{
    public class DeliverymanRepo
    {
        private readonly BookExchangeDbContext db;
        public DeliverymanRepo()
        {
            db = new BookExchangeDbContext();

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public string Get(LoginViewModel model)
        {
            UserDetail emptype = new UserDetail();
            var users = db.Logins.FirstOrDefault(x => x.email == model.Email && x.password == model.Password && x.type == model.type);
            if (users != null)
            {
                return users.email;
            }
            else
            {
                return null;
            }
        }
        public int GetUserId(LoginViewModel model)
        {
            var users = db.Logins.FirstOrDefault(x => x.email == model.Email && x.password == model.Password && x.type == model.type);
            if (users != null)
            {
                return users.id;
            }
            else
            {
                return 0;
            }
        }

        public int GetType(LoginViewModel model)
        {
            var users = db.Logins.FirstOrDefault(x => x.email == model.Email && x.password == model.Password);
            if (users != null)
            {
                return users.type;
            }
            else
            {
                return 0;
            }
        }




        public DeleveryManDetail getDetail(string DEmail)
        {
            var users = db.DeleveryManDetails.FirstOrDefault(x => x.email == DEmail);
            DeleveryManDetail u = new DeleveryManDetail();
            u.name = users.name;
            u.email = users.email;
            u.phone = users.phone;
            u.adress = users.adress;
        
            return u;
        
        }

        
        public int DEdit(string DEmail,DeleveryManDetail dmodel)
        {
           using (BookExchangeDbContext db = new BookExchangeDbContext())
           {
               DeleveryManDetail d = db.DeleveryManDetails.SingleOrDefault(x => x.email == DEmail);

                d.name = dmodel.name;
                d.phone = dmodel.phone;
                d.adress = dmodel.adress;

                try
                {
                    db.SaveChanges();
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
        }





      


    }
}