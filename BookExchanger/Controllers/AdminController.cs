using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookExchanger.Repository;

namespace BookExchanger.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdmin repo;

        public AdminController()
        {
            repo = new AdminRepository();
        }

        public ActionResult Index()
        {
            if ((string)Session["AdminMail"] == "admin@gmail.com" && (string)Session["AdminPassword"] == "255242")
                return View();


            return RedirectToAction(nameof(LogOut));
        }

        public ActionResult LogOut()
        {
            Session["AdminMail"] = string.Empty;
            Session["AdminPassword"] = string.Empty;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult AddEmployee()
        {
            if ((string)Session["AdminMail"] == "admin@gmail.com" && (string)Session["AdminPassword"] == "255242")
                return View();

            return RedirectToAction(nameof(LogOut));
        }

        [HttpPost]
        public ActionResult AddEmployee(DeleveryManDetail dm)
        {
            if ((string)Session["AdminMail"] == "admin@gmail.com" && (string)Session["AdminPassword"] == "255242")
            {
                try
                {
                    if (dm.name != null && dm.email != null && dm.adress != null && Convert.ToString(dm.phone) != null)
                    {
                        repo.AddEmployee(dm);
                        repo.SendMail(dm.email);

                        return RedirectToAction(nameof(Index));
                    }

                    return RedirectToAction(nameof(AddEmployee));
                }
                catch (Exception) { return RedirectToAction(nameof(AddEmployee)); }
            }

            return RedirectToAction(nameof(LogOut));
        }

        public ActionResult EmployeeDetails()
        {
            if ((string)Session["AdminMail"] == "admin@gmail.com" && (string)Session["AdminPassword"] == "255242")
            {
                var emp = repo.EmployeeDetails();
                return View(emp);
            }

            return RedirectToAction(nameof(LogOut));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if ((string)Session["AdminMail"] == "admin@gmail.com" && (string)Session["AdminPassword"] == "255242")
                return View(repo.GetEmployeeById(id));

            return RedirectToAction(nameof(LogOut));
        }

        [HttpPost]
        public ActionResult Edit(DeleveryManDetail dm)
        {
            if ((string)Session["AdminMail"] == "admin@gmail.com" && (string)Session["AdminPassword"] == "255242")
            {
                try
                {
                    repo.UpdateEmployee(dm);
                    return RedirectToAction(nameof(EmployeeDetails));
                }
                catch (Exception)
                {
                    return RedirectToAction(nameof(Edit), new { id = dm.id });
                }
            }

            return RedirectToAction(nameof(LogOut));
        }

        public ActionResult Delete(int id)
        {
            if ((string)Session["AdminMail"] == "admin@gmail.com" && (string)Session["AdminPassword"] == "255242")
            {
                repo.DeleteEmployee(id);
                return RedirectToAction(nameof(EmployeeDetails));
            }

            return RedirectToAction(nameof(LogOut));
        }

        public ActionResult Users()
        {
            if ((string)Session["AdminMail"] == "admin@gmail.com" && (string)Session["AdminPassword"] == "255242")
                return View(repo.UserDetails());

            return RedirectToAction(nameof(LogOut));
        }

        [HttpGet]
        public ActionResult Pending()
        {
            if ((string)Session["AdminMail"] == "admin@gmail.com" && (string)Session["AdminPassword"] == "255242")
                return View(repo.PendingBooks());

            return RedirectToAction(nameof(LogOut));
        }

        [HttpPost]
        public ActionResult Pending(string SubmitButton, int[] bId)
        {
            if ((string)Session["AdminMail"] == "admin@gmail.com" && (string)Session["AdminPassword"] == "255242")
            {
                if (bId != null)
                {
                    if (SubmitButton == "Accept")
                    {
                        foreach (var item in bId)
                        {
                            repo.AcceptBook(item);
                        }
                        return RedirectToAction(nameof(Pending));
                    }
                    else if (SubmitButton == "Reject")
                    {
                        foreach (var item in bId)
                        {
                            repo.RejectBook(item);
                        }
                        return RedirectToAction(nameof(Pending));
                    }
                }
                return RedirectToAction(nameof(Pending));
            }

            return RedirectToAction(nameof(LogOut));
        }

        public ActionResult Accept(int id)
        {
            if ((string)Session["AdminMail"] == "admin@gmail.com" && (string)Session["AdminPassword"] == "255242")
            {
                repo.AcceptBook(id);
                return RedirectToAction(nameof(Pending));
            }

            return RedirectToAction(nameof(LogOut));
        }

        public ActionResult Reject(int id)
        {
            if ((string)Session["AdminMail"] == "admin@gmail.com" && (string)Session["AdminPassword"] == "255242")
            {
                repo.RejectBook(id);
                return RedirectToAction(nameof(Pending));
            }

            return RedirectToAction(nameof(LogOut));
        }

        [HttpGet]
        public ActionResult Rejected()
        {
            if ((string)Session["AdminMail"] == "admin@gmail.com" && (string)Session["AdminPassword"] == "255242")
                return View(repo.RejectedBooks());

            return RedirectToAction(nameof(LogOut));
        }

        [HttpPost]
        public ActionResult Rejected(int[] bId)
        {
            if ((string)Session["AdminMail"] == "admin@gmail.com" && (string)Session["AdminPassword"] == "255242")
            {
                if (bId != null)
                {
                    foreach (var item in bId)
                    {
                        repo.AcceptBook(item);
                    }
                    return RedirectToAction(nameof(Pending));
                }
                return RedirectToAction(nameof(Rejected));
            }

            return RedirectToAction(nameof(LogOut));
        }

        [HttpGet]
        public ActionResult Posting()
        {
            if ((string)Session["AdminMail"] == "admin@gmail.com" && (string)Session["AdminPassword"] == "255242")
                return View(repo.Posting());

            return RedirectToAction(nameof(LogOut));
        }
    }
}