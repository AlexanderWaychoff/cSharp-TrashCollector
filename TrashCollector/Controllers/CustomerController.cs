﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customer
        public ActionResult Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                int searchZipCode = Convert.ToInt32(searchString);
                string dayOfWeek = System.DateTime.Now.DayOfWeek.ToString();
                var customers = db.Customer.Where(s => (s.ZipCode.Equals(searchZipCode)) 
                && (s.RequestedPickUpDay == s.ScheduledPickUpDay || s.RequestedPickUpDay == null ?
                s.ScheduledPickUpDay.ToString().Equals(dayOfWeek) : s.RequestedPickUpDay.ToString().Equals(dayOfWeek))
                && (s.IsOnVacation != true)
                ).ToList();

                List<Customer> filteredCustomers = new List<Customer>();
                foreach (var a in customers)
                {
                    filteredCustomers.Add(a);
                }
                return View(filteredCustomers);
            }
            return View(db.Customer.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            Customer customer = db.Customer.FirstOrDefault(p => p.AccountID == user.Id);
            if(customer != null && customer.AccountID == user.Id)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,StreetAddress,City,StateAbbreviated,ZipCode,IsOnVacation,RequestedPickUpDay,ScheduledPickUpDay,MonthlyCharge,IsAdmin,AccountID")] Customer customer)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (ModelState.IsValid && customer.AccountID != user.Id)
            {
                customer.AccountID = user.Id;
                customer.MonthlyCharge = 15;
                customer.ScheduledPickUpDay = Weekday.Wednesday;
                db.Customer.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(customer);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            if (customer.IsAdmin)
            {
                return View(customer);
            }
            else
            {
                return SingleWeekChange(id);
            }
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,StreetAddress,City,StateAbbreviated,ZipCode,IsOnVacation,RequestedPickUpDay,ScheduledPickUpDay,MonthlyCharge,IsAdmin,AccountID")] Customer customer)
        {

            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(customer);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customer.Find(id);
            db.Customer.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangeStatus()
        {
            //var id = User.Identity.GetUserId();

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            Customer customer = db.Customer.FirstOrDefault(p => p.AccountID == user.Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        public ActionResult MonthlyCharge()
        {
            //var id = User.Identity.GetUserId();

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            Customer customer = db.Customer.FirstOrDefault(p => p.AccountID == user.Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        public ActionResult SingleWeekChange(int? id)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            try
            {
                Customer customer = db.Customer.FirstOrDefault(p => p.AccountID == user.Id);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                return View(customer);
            }
            catch
            {

            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SingleWeekChange([Bind(Include = "CustomerID,FirstName,LastName,StreetAddress,City,StateAbbreviated,ZipCode,IsOnVacation,RequestedPickUpDay,ScheduledPickUpDay,MonthlyCharge,IsAdmin,AccountID")] Customer customer)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            try
            {
                Customer baseCustomer = db.Customer.FirstOrDefault(p => p.AccountID == user.Id);

                db.Customer
                       .Where(f => f.CustomerID.Equals(customer.CustomerID))
                       .ToList()
                       .ForEach(f => f.RequestedPickUpDay = customer.RequestedPickUpDay);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");

            }
            catch
            {

            }
            return View(customer);
        }

        public ActionResult ScheduledChange(int? id)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            try
            {
                Customer customer = db.Customer.FirstOrDefault(p => p.AccountID == user.Id);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                return View(customer);
            }
            catch
            {

            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ScheduledChange([Bind(Include = "CustomerID,FirstName,LastName,StreetAddress,City,StateAbbreviated,ZipCode,IsOnVacation,RequestedPickUpDay,ScheduledPickUpDay,MonthlyCharge,IsAdmin,AccountID")] Customer customer)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            try
            {
                Customer baseCustomer = db.Customer.FirstOrDefault(p => p.AccountID == user.Id);
                db.Customer
                       .Where(f => f.CustomerID.Equals(customer.CustomerID))
                       .ToList()
                       .ForEach(f => f.ScheduledPickUpDay = customer.ScheduledPickUpDay);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");

            }
            catch
            {

            }
            return View(customer);
        }

        public ActionResult Vacation(int? id)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            try
            {
                Customer customer = db.Customer.FirstOrDefault(p => p.AccountID == user.Id);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                return View(customer);
            }
            catch
            {

            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vacation([Bind(Include = "CustomerID,FirstName,LastName,StreetAddress,City,StateAbbreviated,ZipCode,IsOnVacation,RequestedPickUpDay,ScheduledPickUpDay,MonthlyCharge,IsAdmin,AccountID")] Customer customer)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            try
            {
                Customer baseCustomer = db.Customer.FirstOrDefault(p => p.AccountID == user.Id);

                db.Customer
                       .Where(f => f.CustomerID.Equals(customer.CustomerID))
                       .ToList()
                       .ForEach(f => f.IsOnVacation = customer.IsOnVacation);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
                
            }
            catch
            {

            }
            return View(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
