using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElderSourceApp.Models;

namespace ElderSourceApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CompanyController : Controller
    {
        private CompanyContext db = new CompanyContext();

        // GET: Company
        public ActionResult Index(String searchstring)
        {
            var contact = from c in db.Company
                          select c;
            if (!String.IsNullOrEmpty(searchstring))
            {
                contact = contact.Where(s => s.CompanyName.Contains(searchstring));
            }

            return View(contact);
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
        public ActionResult Create([Bind(Include = "CompanyModelID,CompanyName,City,State,ZipCode,Address,CompanyType,Phone,HasSymbol,EmployeesTrained,HasPolicies,HasDeclaration,InArrears, LastPaidDate, description, Notes")] CompanyModel companyModel)
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
        public ActionResult Edit([Bind(Include = "CompanyModelID,CompanyName,City,State,ZipCode,Address,CompanyType,Phone,HasSymbol,EmployeesTrained,HasPolicies,HasDeclaration,InArrears, LastPaidDate, description, Notes")] CompanyModel companyModel)
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

        public ActionResult Search(string companyName = null, string companyType = null, string city = null, string zipCode = null)
        {
            var model =
               db.Company
               .OrderByDescending(r => r.CompanyName)
               .Where(r => !r.InArrears)
               .Where(r => r.EmployeesTrained)
               .Where(r => r.HasPolicies)
               .Where(r => r.HasSymbol)
               .Where(r => r.HasDeclaration)
               .Where(r => companyName == null || r.CompanyName.StartsWith(companyName))
               .Where(r => companyType == null || r.CompanyType.StartsWith(companyType))
               .Where(r => city == null || r.City.StartsWith(city))
               .Where(r => zipCode == null || r.ZipCode.StartsWith(zipCode))
               .Select(r => new CompanyListViewModel
               {
                   CompanyModelID = r.CompanyModelID,
                   CompanyName = r.CompanyName,
                   City = r.City,
                   State = r.State,
                   ZipCode = r.ZipCode,
                   CompanyType = r.CompanyType,
                   Phone = r.Phone,
                   HasSymbol = r.HasSymbol,
                   EmployeesTrained = r.EmployeesTrained,
                   HasPolicies = r.HasPolicies,
                   HasDeclaration = r.HasDeclaration,
                   InArrears = r.InArrears
               });

            return View(model);
        }

    }
}
