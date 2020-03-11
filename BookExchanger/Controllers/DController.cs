using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookExchanger.Models;
using BookExchanger.Repository;

namespace BookExchanger.Controllers
{
    public class DController : Controller
    {
        Users empRepo = new Users();
        DeliverymanRepo DRepo = new DeliverymanRepo();
        BooksRepo bookRepo = new BooksRepo();

        // GET: D
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult UserProfile(string DEmail)
        {
            return View(DRepo.getDetail(DEmail));
        }

        public ActionResult Edit(string DEmail)
        {
            return View(DRepo.getDetail(DEmail));
        }

        [HttpPost]
        public ActionResult Edit(string DEmail,DeleveryManDetail D)
        {
            
                int x = DRepo.DEdit(DEmail,D);
                if (x == 1)
                {
                    return View("Index");
                }
                else
                {

                    return Content("Problem Occured");
                }
    
        }


        public ActionResult SellPostList()
        {
            return View(bookRepo.SellPosts());
        }

        public ActionResult ReceivedBooks()
        {
            return View(bookRepo.receivedBooks());
        }
        public ActionResult DeliverInit(SellPostedModel smodel,int BookId)
        {
            int x = bookRepo.DeliveredStatus(smodel, BookId);
            if (x == 1)
            {
                return View("Index");
            }
            else
            {

                return Content("Problem Occured");
            }
        }

        public ActionResult Deliver(SellPostedModel smodel, int BookId)
        {
            int x = bookRepo.Delivered(smodel, BookId);
            if (x == 1)
            {
                return View("Index");
            }
            else
            {

                return Content("Problem Occured");
            }
        }

        public ActionResult Details(int BookId)
        {
            return View(bookRepo.DeliveryInfo(BookId));
        }


        public ActionResult Logout()
        {
            if (Session["DEmail"] != null && Session["DId"] != null)
            {
                Session["DEmail"] = string.Empty;
                Session["DId"] = string.Empty;

                return RedirectToAction("Index","Home");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}