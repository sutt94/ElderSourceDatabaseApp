using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElderSourceApp.Models;
using PagedList;

using System.Data.Entity.Infrastructure;


namespace ElderSourceApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CompanyController : Controller
    {
        private CompanyContext db = new CompanyContext();

        // GET: Company
        public ActionResult Index(String searchstring, String sortOrder, String currentFilter, int? page)
        {
            
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewBag.CitySortParm = String.IsNullOrEmpty(sortOrder) ? "City" : "";
            ViewBag.ZipSortParm = String.IsNullOrEmpty(sortOrder) ? "Zip Code" : "";
            ViewBag.ServiceSortParm = String.IsNullOrEmpty(sortOrder) ? "Service" : "";
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Last Paid Date" : "";


            if (searchstring != null)
            {
                page = 1;
            }
            else
            {
                searchstring = currentFilter;
            }

            ViewBag.CurrentFilter = searchstring;

            var contacts = from c in db.Company
                           select c;

            if (!String.IsNullOrEmpty(searchstring))
            {
                contacts = contacts.Where(s => s.CompanyName.ToUpper().Contains(searchstring.ToUpper())
                             || s.ZipCode.ToUpper().Contains(searchstring.ToUpper())
                             ||
                             s.City.ToUpper().Contains(searchstring.ToUpper()) ||
                             s.State.ToUpper().Contains(searchstring.ToUpper()) || 
                             s.Phone.ToUpper().Contains(searchstring.ToUpper()) ||
                             s.LastPaidDate.ToString().Contains(searchstring.ToUpper()));
               

            }
            switch (sortOrder)
            {
                case "Name":
                    contacts = contacts.OrderBy(s => s.CompanyName);
                    break;
                case "City":
                    contacts = contacts.OrderBy(s => s.City);
                    break;
                case "Zip Code":
                    contacts = contacts.OrderBy(s => s.ZipCode);
                    break;
                case "Service":
                    contacts = contacts.OrderBy(s => s.CompanyType);
                    break;
                case "Last Paid Date":
                    contacts = contacts.OrderByDescending(s => s.LastPaidDate);
                    break;
                default:  // Name ascending 
                    contacts = contacts.OrderBy(s => s.CompanyName);
                    break;
            }





            return View(contacts);

        }

        // GET: Company/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyModel companyModel = db.Company.Find(id);
            if (companyModel == null)
            {
                return HttpNotFound();
            }
            return View(companyModel);
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyModelID,CompanyName,City,State,ZipCode,CompanyType,Phone,HasSymbol,EmployeesTrained,HasPolicies,HasDeclaration,InArrears, LastPaidDate, description")] CompanyModel companyModel)
        {
            if (ModelState.IsValid)
            {
                db.Company.Add(companyModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(companyModel);
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyModel companyModel = db.Company.Find(id);
            if (companyModel == null)
            {
                return HttpNotFound();
            }
            return View(companyModel);
        }

        // POST: Company/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyModelID,CompanyName,City,State,ZipCode,CompanyType,Phone,HasSymbol,EmployeesTrained,HasPolicies,HasDeclaration,InArrears,LastPaidDate, description")] CompanyModel companyModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(companyModel);
        }

        // GET: Company/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyModel companyModel = db.Company.Find(id);
            if (companyModel == null)
            {
                return HttpNotFound();
            }
            return View(companyModel);
        }

        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompanyModel companyModel = db.Company.Find(id);
            db.Company.Remove(companyModel);
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
