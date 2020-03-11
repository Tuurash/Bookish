using BookExchanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookExchanger.Repository
{
    public class BooksRepo
    {
        Users u;
        private readonly BookExchangeDbContext db;

        public BooksRepo()
        {
            db = new BookExchangeDbContext();
            u = new Users();
        }
        public IEnumerable<BookDetail> bookDetails(int userId)
        {
            return db.BookDetails.Where(x => x.status == 1 &&x.UserId!=userId).ToList();
        }

        public int InsertBook(BookDetails bookDetails,int id)
        {

            Console.WriteLine(""+id);
            BookDetail book = new BookDetail();
            book.Author = bookDetails.Author;
            book.Edition = bookDetails.Edition;
            book.Point = bookDetails.point;
            book.Title = bookDetails.Title;
            book.UsedDate = bookDetails.UsedDate;
            book.UserId = id;
            book.status = 0;
            db.BookDetails.Add(book);
            db.SaveChanges();
            return 1;

        }

        public BookDetail getBookInfo(int id)
        {
            BookDetail book = new BookDetail();
            book = db.BookDetails.FirstOrDefault(x=>x.Id==id);
            return book;
        }

        public IEnumerable<RequestBook> requestedBookDetails()
        {
            return db.Set<RequestBook>().ToList();
        }
        public int InsertRequestBook(RequestBookViewModel model,int id)
        {
            RequestBook book = new RequestBook();
            book.Author = model.Author;
            book.point = model.point;
            book.Title = model.Tittle;
            book.userId = id;


            try
            {
                db.RequestBook.Add(book);

                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }

        }

        public int CreateSellpost(SellPostedModel model, int id)
        {
            SellPosting book = new SellPosting();
            book.BookId = model.BookId;
            book.offeredPoint = model.offeredPoint;
            book.SellerId = model.SellerId;
            book.BuyerId= model.BuyerId;
            book.Status = model.Status;

            try
            {

                db.SellPostings.Add(book);

                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }

        }

        public IEnumerable<SellPosting> SellPosts()
        {
            return db.SellPostings.Where(x => x.Status == 1).ToList();
        }

        public IEnumerable<SellPosting> receivedBooks()
        {
            return db.SellPostings.Where(x => x.Status == 3).ToList();
        }


        public int DeliveredStatus(SellPostedModel model, int BookId)
        {

            
            SellPosting book = db.SellPostings.FirstOrDefault(x=>x.BookId==BookId);

            book.Status = model.Status = 3;

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
        public int Delivered(SellPostedModel model, int BookId)
        {


            //SellPosting book = db.SellPostings.FirstOrDefault(x => x.BookId == BookId);

            SellPosting book=db.SellPostings.FirstOrDefault(x => x.Status == 3);

            book.Status = model.Status = 4;

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

        public SellPosting DeliveryInfo(int BookId)
        {
       
            SellPosting book = db.SellPostings.FirstOrDefault(x => x.BookId == BookId);
            SellPosting u = new SellPosting();
            u.BookId = book.BookId;
            u.BuyerId = book.BuyerId;
            u.SellerId = book.SellerId;
            

            return u;



        }

        public void AddCheckOut(int id,int buyerId)
        {
            BookDetail b = db.BookDetails.SingleOrDefault(x => x.Id == id);
            SellPosting sellPosting = new SellPosting();
            if (b != null)
            {
                sellPosting.BookId = b.Id;
            sellPosting.BuyerId = buyerId;
            sellPosting.offeredPoint = b.Point;
            sellPosting.SellerId = b.UserId;
            sellPosting.Status = 0;

                db.SellPostings.Add(sellPosting);

                db.SaveChanges();
            }
        }

        public SellPosting getCheckout(int id)
        {
            SellPosting book = db.SellPostings.FirstOrDefault(x => x.BuyerId == id&&x.Status==0);
            return book;
        }
        public SellPosting getHistory(int id)
        {
            SellPosting book = db.SellPostings.FirstOrDefault(x => x.BuyerId == id && x.Status >= 1);
            return book;
        }


        public void confirm(int id)
        {
            SellPosting sellPosting = db.SellPostings.FirstOrDefault(x=>x.BookId==id);
            if(sellPosting!=null)
            {
                sellPosting.Status = 1;
                db.SaveChanges();
            }
            BookDetail bookDetail = db.BookDetails.FirstOrDefault(x=>x.Id==id);
            if (bookDetail != null)
            {
                bookDetail.status = 4;
                db.SaveChanges();
            }
            Login l = db.Logins.FirstOrDefault(x => x.id == sellPosting.BuyerId);
            UserDetail buyer = db.UserDetails.FirstOrDefault(x=>x.email==l.email);
            if (buyer.point >= sellPosting.offeredPoint)
            {
                if (buyer != null)
                {

                    buyer.point = buyer.point - sellPosting.offeredPoint;
                    db.SaveChanges();


                }
                Login l2 = db.Logins.FirstOrDefault(x => x.id == sellPosting.SellerId);
                UserDetail seller = db.UserDetails.FirstOrDefault(x => x.email == l2.email);
                if (seller != null)
                {
                    seller.point = seller.point + sellPosting.offeredPoint;
                    db.SaveChanges();
                }

            }
            else
            {
                sellPosting.Status = 0;
                bookDetail.status = 1;
                db.SaveChanges();
            }
        }



    }
}
