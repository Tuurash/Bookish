using BookExchanger.Models;
using BookExchanger.Models.UsersModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookExchanger.Repository
{
    public class Users
    {
        private readonly BookExchangeDbContext db;
        public Users()
        {
            db = new BookExchangeDbContext();
            
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public string Get(LoginViewModel model)
        {
            var users = db.Logins.FirstOrDefault(x => x.email == model.Email && x.password == model.Password);
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
            var users = db.Logins.FirstOrDefault(x => x.email == model.Email && x.password == model.Password);
            if (users != null)
            {
                return users.id;
            }
            else
            {
                return 0;
            }
        }

        public int Insert(RegisterViewModel model)
        {
            Login login = new Login();
            login.email = model.Email;
            login.password = model.Password;
            login.type = 0;

            db.Logins.Add(login);
            db.SaveChanges();
            return 1;

        }

        public int Insert2(UserDetailsInsertModel model,string Email)
        {
            UserDetail userDetail = new UserDetail();
            userDetail.adress = model.Address;
            userDetail.email = Email;
            userDetail.name = model.Name;
            userDetail.phone = model.Phone;
            userDetail.point = 50;
            try
            {
                db.UserDetails.Add(userDetail);
                
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }

        }

        public UserDetailsView getDetail(string email)
        {
            UserDetail users = db.UserDetails.FirstOrDefault(x=>x.email==email);
            UserDetailsView u = new UserDetailsView();
            u.Name = users.name;
            u.Email = users.email;
            u.Phone = users.phone;
            u.Address = users.adress;
            u.Point = users.point;

            return u;

        }


        public int editUserProfile(UserDetailsView model,string email)
        {

            UserDetail userDetail = db.UserDetails.FirstOrDefault(x => x.email == email);
            userDetail.adress = model.Address;
            userDetail.email = model.Email;
            userDetail.name = model.Name;
            userDetail.phone = model.Phone;
            
            try
            {
               // db.UserDetails.Add(userDetail);

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