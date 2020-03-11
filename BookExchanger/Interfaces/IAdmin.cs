using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchanger.Repository
{
    public interface IAdmin
    {
        void AddEmployee(DeleveryManDetail dm);
        void EditEmployee(DeleveryManDetail dm);
        void DeleteEmployee(DeleveryManDetail dm);
        IEnumerable<DeleveryManDetail> EmployeeDetails();
        void SendMail(string mail);
        DeleveryManDetail GetEmployeeById(int id);
        void UpdateEmployee(DeleveryManDetail dm);

        bool IsEmailExist(string mail);

        void DeleteEmployee(int id);

        IEnumerable<UserDetail> UserDetails();

        List<BookDetail> PendingBooks();

        List<BookDetail> RejectedBooks();

        void AcceptBook(int id);
        void RejectBook(int id);

        IEnumerable<SellPosting> Posting();
    }
}
