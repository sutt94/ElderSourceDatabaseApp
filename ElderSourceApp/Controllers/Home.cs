using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElderSourceApp.Controllers
{
    public class Home : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}