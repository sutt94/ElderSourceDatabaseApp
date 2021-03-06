﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElderSourceApp.Models;
using System.Net;
using PagedList;
using PagedList.Mvc;

namespace ElderSourceApp.Controllers
{
    [Authorize(Roles = "Admin, AccountManager, Employee")]
    [HandleError(ExceptionType = typeof(Exception), View = "Error")]
    public class SearchController : Controller
    {
        // GET: Search
        CompanyContext _db = new CompanyContext();
        private object db;

        public ActionResult Index(int page = 1, string companyName = null, string companyType = null, string city = null, string zipCode = null, string Address = null)
        {
            var model =
               _db.Company
               .OrderByDescending(r => r.CompanyName)
               .Where(r => !r.InArrears)
               .Where(r => r.EmployeesTrained)
               .Where(r => r.HasPolicies)
               .Where(r => r.HasSymbol)
               .Where(r => r.HasDeclaration)
               .Where(r => companyName == null || r.CompanyName.Contains(companyName))
               .Where(r => companyType == null || r.CompanyType.Contains(companyType))
               .Where(r => city == null || r.City.Contains(city))
               .Where(r => zipCode == null || r.ZipCode.Contains(zipCode))
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
               }).ToPagedList(page, 10);
            string val1 = Request.Form["companyName"];
            string val2 = Request.Form["companyType"];
            string val3 = Request.Form["city"];
            string val4 = Request.Form["zipCode"];

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Companies", model);
            }

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyModel companyModel = _db.Company.Find(id);
            if (companyModel == null)
            {
                return HttpNotFound();
            }
            return View(companyModel);
        }
    }
}