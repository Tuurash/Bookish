using BookExchanger.Models;
using BookExchanger.Models.UsersModel;
using BookExchanger.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookExchanger.Controllers
{
    public class HomeController : Controller
    {
        Users empRepo = new Users();
        BooksRepo bookRepo = new BooksRepo();
        DeliverymanRepo DRepo = new DeliverymanRepo();

        string email;
        int id;
        int type;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            try
            {
                if (model.Email == "admin@gmail.com" && model.Password == "255242")
                {
                    Session["AdminMail"] = "admin@gmail.com";
                    Session["AdminPassword"] = "255242";
                    return RedirectToAction("Index", "Admin");
                } 

                string x = empRepo.Get(model);
                id = empRepo.GetUserId(model);
                type = DRepo.GetType(model);
                if (x != null)
                {
                   if (type == 2)
                   {
                       Session["DEmail"] = x;
                       Session["DId"] = id;
                       return RedirectToAction("Index", "D");
                   }

                    if (type == 0)
                    {
                        Session["EmailL"] = x;
                        Session["Id"] = id;
                        email = x;
                        return RedirectToAction("AllBookDetails");
                    }
                    else
                    {
                        return Content("Wrong Email or Password");
                    }
                    //return Content("id="  +id);

                }
                else
                {
                    return Content("Wrong Email or Password");
                }
            }
            catch (Exception e)
            {
                return Content("Wrong Email or Password");
            }
        }



        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel registerModel)
        {
            try
            {
                int x = empRepo.Insert(registerModel);
                Session["Email"] = registerModel.Email;
                if (x == 1)
                {
                    return RedirectToAction("Register2");
                }
                else
                {
                    return Content("Faild");
                }
            }
            catch(Exception)
            {
                return RedirectToAction("Register");
            }
        }

        public ActionResult Register2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register2(UserDetailsInsertModel model)
        {
            try
            {
                int x = empRepo.Insert2(model, Session["Email"].ToString());
                if (x == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("Faild");
                }
            }
            catch(Exception)
            {
                return RedirectToAction("Register2");
            }
        }
        [HttpGet]
        public ActionResult AllBookDetails()
        {

            return View(bookRepo.bookDetails(Convert.ToInt32(Session["Id"])));
        }

        [HttpGet]

        public ActionResult SellBook()
        {
            return View();
        }
        [HttpPost]

        public ActionResult SellBook(BookDetails bookDetails)
        {

            int x = bookRepo.InsertBook(bookDetails, Convert.ToInt32(Session["Id"]));

            if (x == 1)
            {
                return RedirectToAction("AllBookDetails");
            }
            else
            {
                return Content("Faild");
            }
        }


        [HttpGet]

        public ActionResult UserProfile()
        {

            var user = empRepo.getDetail(Session["EmailL"].ToString());


            return View(user);
        }

        [HttpGet]

        public ActionResult BookDetails(int id)
        {

            var book = bookRepo.getBookInfo(id);
            BookDetails bookDetails = new BookDetails();
            bookDetails.Author = book.Author;
            bookDetails.Edition = book.Edition;
            bookDetails.point = book.Point;
            bookDetails.Title = book.Title;
            bookDetails.UsedDate = book.UsedDate;
            return View(bookDetails);
        }

        [HttpGet]
        public ActionResult RequestedBook()
        {
            return View(bookRepo.requestedBookDetails());
        }

        [HttpGet]
        public ActionResult RequestBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RequestBook(RequestBookViewModel registerModel)
        {
            int x = bookRepo.InsertRequestBook(registerModel, Convert.ToInt32(Session["id"]));

            if (x == 1)
            {
                return RedirectToAction("AllBookDetails");
            }
            else
            {
                return Content("Faild");
            }
        }
        [HttpGet]
        public ActionResult EditProfile()
        {
            var user = empRepo.getDetail(Session["EmailL"].ToString());
            return View(user);
        }

        [HttpPost]
        public ActionResult EditProfile(UserDetailsView detailsView)
        {
            int x = empRepo.editUserProfile(detailsView,Session["EmailL"].ToString());
            if(x==1)
            {
                return View("AllBookDetails ");
            }
            else
            {

                return Content("Oops");
            }
        }

        public ActionResult BuyNow(int id)
        {
            bookRepo.AddCheckOut(id, Convert.ToInt32(Session["id"]));
            return View("CheckOut");
        }
        [HttpGet]

        public ActionResult CheckOut()
        {
            SellPosting sellPosting = bookRepo.getCheckout(Convert.ToInt32(Session["id"]));
            if (sellPosting != null)
            {
                BookDetail bookDetail = bookRepo.getBookInfo(sellPosting.BookId);
                CheckOutViewModel checkOutView = new CheckOutViewModel();
                checkOutView.BookTitle = bookDetail.Title;
                checkOutView.offeredPoint = sellPosting.offeredPoint;

                Session["SellId"] =bookDetail.Id ;

                return View(checkOutView);
            }
            else
            {
                return View();
            }
        }

        public ActionResult ConfirmBuy()
        {
            bookRepo.confirm(Convert.ToInt32(Session["SellId"]));
            Session["SellId"] = "";
            return View("CheckOut");
            
        }

        public ActionResult History()
        {
            SellPosting sellPosting = bookRepo.getHistory(Convert.ToInt32(Session["Id"]));
            
            if(sellPosting!=null)
            {
                return View(sellPosting);
            }
            else
            {
                return View("CheckOut");
            }
            
            
        }
        public ActionResult LogOut()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }

    }
}