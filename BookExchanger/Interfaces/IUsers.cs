using BookExchanger.Models.UsersModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchanger.Repository
{
    interface IUsers
    {
        IEnumerable<LoginViewModel> GetAll();
        int Get(LoginViewModel model);
        void Insert(LoginViewModel model);
        void Update(LoginViewModel model);
        void Delete(int id);
        
    }
}
