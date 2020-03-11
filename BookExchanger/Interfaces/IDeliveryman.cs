using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookExchanger.Models.UsersModel;

namespace BookExchanger.Repository
{
    interface IDeliveryman
    {
        IEnumerable<LoginViewModel> GetAll();
        int Get(LoginViewModel model);
        void Insert(LoginViewModel model);
        void Update(LoginViewModel model);
        void Delete(int id);
    }
}