using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            var customer = db.Customer.Include(c => c.City).Include(c => c.StateAbbreviation).Include(c => c.Zipcode);
            return View(customer.ToList());
        }

        // GET: Customers/Details/5
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

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.CityID = new SelectList(db.City, "CityID", "Name");
            ViewBag.StateAbbreviatedID = new SelectList(db.StateAbbreviated, "StateAbbreviatedID", "TwoLetterAbbreviation");
            ViewBag.ZipCodeID = new SelectList(db.ZipCode, "ZipCodeID", "ZipCodeID");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,FirstName,LastName,StreetAddress,CityID,StateAbbreviatedID,ZipCodeID,Email,IsOnVacation,RequestedPickUpDay,ScheduledPickUpDay,MonthlyCharge,IsAdmin")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customer.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityID = new SelectList(db.City, "CityID", "Name", customer.CityID);
            ViewBag.StateAbbreviatedID = new SelectList(db.StateAbbreviated, "StateAbbreviatedID", "TwoLetterAbbreviation", customer.StateAbbreviatedID);
            ViewBag.ZipCodeID = new SelectList(db.ZipCode, "ZipCodeID", "ZipCodeID", customer.ZipCodeID);
            return View(customer);
        }

        // GET: Customers/Edit/5
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
            ViewBag.CityID = new SelectList(db.City, "CityID", "Name", customer.CityID);
            ViewBag.StateAbbreviatedID = new SelectList(db.StateAbbreviated, "StateAbbreviatedID", "TwoLetterAbbreviation", customer.StateAbbreviatedID);
            ViewBag.ZipCodeID = new SelectList(db.ZipCode, "ZipCodeID", "ZipCodeID", customer.ZipCodeID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,FirstName,LastName,StreetAddress,CityID,StateAbbreviatedID,ZipCodeID,Email,IsOnVacation,RequestedPickUpDay,ScheduledPickUpDay,MonthlyCharge,IsAdmin")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityID = new SelectList(db.City, "CityID", "Name", customer.CityID);
            ViewBag.StateAbbreviatedID = new SelectList(db.StateAbbreviated, "StateAbbreviatedID", "TwoLetterAbbreviation", customer.StateAbbreviatedID);
            ViewBag.ZipCodeID = new SelectList(db.ZipCode, "ZipCodeID", "ZipCodeID", customer.ZipCodeID);
            return View(customer);
        }

        // GET: Customers/Delete/5
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

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customer.Find(id);
            db.Customer.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
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
