using BookExchangerWebApi.Models;
using BookExchangerWebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookExchangerWebApi.Repository
{
    public class BookRepo : Repository<Book>, IBookRepo
    {

        BookExchangerDBEntities context;

        public BookRepo()
        {
            context = new BookExchangerDBEntities();
        }
        public IEnumerable<Book> GetBooksUserId(int userId)
        {
            return context.Books.Where(x => x.UserId == userId).ToList();
        }

        public IEnumerable<BooksDTO> AcceptedBooks()
        {

            var books = context.Books.Where(x => x.Status == 1).OrderByDescending(x => x.BookId).ToList();

            var booksToReturn = books.Select(x => new BooksDTO()
            {
                BookId = x.BookId,
                BookTitle = x.BookTitle,
                BookAuthor = x.BookAuthor,
                BookEdition = x.BookEdition,
                Point = x.Point,
                UploadedBy = context.Users.Where(y => y.UserId == x.UserId).Select(z => z.Email).FirstOrDefault()
            });

            return booksToReturn;
        }

        public IEnumerable<BooksDTO> PendingBooks()
        {
            var books = context.Books.Where(x => x.Status == 0).OrderByDescending(x => x.BookId).ToList();

            var booksToReturn = books.Select(x => new BooksDTO()
            {
                BookId = x.BookId,
                BookTitle = x.BookTitle,
                BookAuthor = x.BookAuthor,
                BookEdition = x.BookEdition,
                Point = x.Point,
                UploadedBy = context.Users.Where(y => y.UserId == x.UserId).Select(z => z.Email).FirstOrDefault()
            });

            return booksToReturn;
        }

        public IEnumerable<BooksDTO> RejectedBooks()
        {
            var books = context.Books.Where(x => x.Status == 2).OrderByDescending(x => x.BookId).ToList();

            var booksToReturn = books.Select(x => new BooksDTO()
            {
                BookId = x.BookId,
                BookTitle = x.BookTitle,
                BookAuthor = x.BookAuthor,
                BookEdition = x.BookEdition,
                Point = x.Point,
                UploadedBy = context.Users.Where(y => y.UserId == x.UserId).Select(z => z.Email).FirstOrDefault()
            });

            return booksToReturn;
        }


        public int TotalAcceptedBooks()
        {
            return context.Books.Count(x => x.Status == 1);
        }

        public int TotalPendingBooks()
        {
            return context.Books.Count(x => x.Status == 0);
        }

        public int TotalRejectedBooks()
        {
            return context.Books.Count(x => x.Status == 2);
        }

        public void Accept(int id)
        {
            var b = context.Books.SingleOrDefault(x => x.BookId == id);

            if (b != null)
            {
                b.Status = 1;
                context.SaveChanges();
            }
        }

        public void Reject(int id)
        {
            var b = context.Books.SingleOrDefault(x => x.BookId == id);

            if (b != null)
            {
                b.Status = 2;
                context.SaveChanges();
            }
        }

        public IEnumerable<TopUploaderDTO> TopUploader()
        {
            var top = context.Books.Where(x => x.Status == 1).GroupBy(x => x.UserId).OrderByDescending(g => g.Count()).Take(10);

            var data = new List<TopUploaderDTO>();

            foreach (var item in top)
            {
                int id = Convert.ToInt32(item.Key.ToString());
                var email = context.Users.Where(x => x.UserId == id).Select(y => y.Email).FirstOrDefault();
                data.Add(new TopUploaderDTO { Email = email, Count = item.Count() });
            }

            return data.ToList();
        }

        public IEnumerable<TopSellerDTO> TopSeller()
        {
            var top = context.SellPostings.GroupBy(x => x.SellerId).OrderByDescending(g => g.Count()).Take(10);

            var data = new List<TopSellerDTO>();

            foreach (var item in top)
            {
                int id = Convert.ToInt32(item.Key.ToString());
                var email = context.Users.Where(x => x.UserId == id).Select(y => y.Email).FirstOrDefault();
                data.Add(new TopSellerDTO { Email = email, Count = item.Count() });
            }

            return data.ToList();
        }

        public IEnumerable<TopBuyerDTO> TopBuyer()
        {
            var top = context.SellPostings.GroupBy(x => x.BuyerId).OrderByDescending(g => g.Count()).Take(10);

            var data = new List<TopBuyerDTO>();

            foreach (var item in top)
            {
                int id = Convert.ToInt32(item.Key.ToString());
                var email = context.Users.Where(x => x.UserId == id).Select(y => y.Email).FirstOrDefault();
                data.Add(new TopBuyerDTO { Email = email, Count = item.Count() });
            }

            return data.ToList();
        }

    }
}