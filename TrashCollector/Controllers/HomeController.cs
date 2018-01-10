using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            try
            {
                if (user.Email.Equals("cabbagefat@gmail.com")) //redirect to Contact page
                {
                    return RedirectToAction("Contact");
                }
            }
            catch
            {

            }
            //Customer customer = db.Customer.FirstOrDefault(p => p.AccountID == user.Id);
            return View();
        }

        public ActionResult About()
        {
            if(Request.IsAuthenticated)
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Home.";

            return View();
        }
    }
}