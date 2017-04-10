using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElderSourceApp.Models;
using System.Net;
using PagedList;

namespace ElderSourceApp.Controllers
{
    [Authorize(Roles = "Admin, AccountManager, Employee")]
    public class SearchController : Controller
    {
        // GET: Search
        CompanyContext db = new CompanyContext();
        

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
                             || s.ZipCode.ToUpper().Contains(searchstring.ToUpper()) ||
                             s.City.ToUpper().Contains(searchstring.ToUpper()) |
                             s.State.ToUpper().Contains(searchstring.ToUpper()) | 
                             s.Phone.ToUpper().Contains(searchstring.ToUpper()) |
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
    }
}

