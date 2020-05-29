using BookExchangerWebApi.Models;
using BookExchangerWebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchangerWebApi.Repository
{
    interface IUsersRepo: IRepository<User>
    {
        User GetUserByEmail(string mail);
        int TotalUsers();
        IEnumerable<UserDTO> AllUsers();
        void DeleteByEmail(string mail);

        int GetStatus(string mail);

        int GetPoint(int id);

        int PutPoint(int id,int point);
    }
}
