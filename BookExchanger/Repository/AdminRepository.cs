using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace BookExchanger.Repository
{
    public class AdminRepository : IAdmin
    {
        private readonly BookExchangeDbContext dbContext;
        private Random rand = new Random();

        public AdminRepository()
        {
            dbContext = new BookExchangeDbContext();
        }

        public void AddEmployee(DeleveryManDetail dm)
        {
            dbContext.DeleveryManDetails.Add(dm);
            dbContext.SaveChanges();
        }

        public void SendMail(string mail)
        {
            int password = rand.Next(1111, 9999);

            try
            {
                using (MailMessage mm = new MailMessage("turash1155049@gmail.com", mail))
                {
                    mm.Subject = "Book Exchange Login";
                    mm.Body = "Email: " + mail + Environment.NewLine + "Password: " + password;

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
                Login li = new Login { email = mail, password = Convert.ToString(password), type = 2 };

                dbContext.Logins.Add(li);
                dbContext.SaveChanges();
            }
        }

        public void DeleteEmployee(DeleveryManDetail dm)
        {
            throw new NotImplementedException();
        }

        public void EditEmployee(DeleveryManDetail dm)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DeleveryManDetail> EmployeeDetails()
        {
            var mans = dbContext.DeleveryManDetails;

            return mans;
        }

        public DeleveryManDetail GetEmployeeById(int id)
        {
            return dbContext.DeleveryManDetails.SingleOrDefault(x => x.id == id);
        }

        public void UpdateEmployee(DeleveryManDetail dm)
        {
            DeleveryManDetail d = dbContext.DeleveryManDetails.SingleOrDefault(x => x.id == dm.id);

            d.name = dm.name;
            d.email = dm.email;
            d.phone = dm.phone;
            d.adress = dm.adress;

            dbContext.SaveChanges();
        }

        public bool IsEmailExist(string email)
        {
            DeleveryManDetail d = dbContext.DeleveryManDetails.SingleOrDefault(x => x.email == email);

            if (d != null)
                return true;

            return false;
        }

        public void DeleteEmployee(int id)
        {
            DeleveryManDetail d = dbContext.DeleveryManDetails.SingleOrDefault(x => x.id == id);
            Login l = dbContext.Logins.SingleOrDefault(x => x.email == d.email);

            dbContext.Logins.Remove(l);
            dbContext.SaveChanges();
            dbContext.DeleveryManDetails.Remove(d);
            dbContext.SaveChanges();
        }

        public IEnumerable<UserDetail> UserDetails()
        {
            return dbContext.UserDetails;
        }

        public List<BookDetail> PendingBooks()
        {
            return dbContext.BookDetails.Where(x => x.status == 0).ToList();
        }

        public void AcceptBook(int id)
        {
            BookDetail b = dbContext.BookDetails.SingleOrDefault(x => x.Id == id);

            if (b != null)
            {
                b.status = 1;
                dbContext.SaveChanges();
            }
        }

        public void RejectBook(int id)
        {
            BookDetail b = dbContext.BookDetails.SingleOrDefault(x => x.Id == id);

            if (b != null)
            {
                b.status = 2;
                dbContext.SaveChanges();
            }
        }

        public List<BookDetail> RejectedBooks()
        {
            return dbContext.BookDetails.Where(x => x.status == 2).ToList();
        }

        public IEnumerable<SellPosting> Posting()
        {
            return dbContext.SellPostings;
        }
    }
}