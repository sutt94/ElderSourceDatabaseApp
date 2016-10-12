using ElderSourceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElderSourceApp.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        public ActionResult Index(UserModel model)
        {
            ElderSourceEntities le = new ElderSourceEntities();
            var s = le.GetLoginInfo(model.UserName, model.Password);

            var item = s.FirstOrDefault();
            if (item == "Success")
            {

                return View("UserLandingView");
            }
            else if (item == "User Does not Exists")

            {
                ViewBag.NotValidUser = item;

            }
            else
            {
                ViewBag.Failedcount = item;
            }

            return View("Index");
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
    }
}