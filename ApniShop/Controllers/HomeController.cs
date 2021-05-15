using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApniShop.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private static string ApiKey = "AIzaSyCr8qSKn57RdUnu_m-o7BlTvC5PZSgnFtg";
        private static string AuthEmail = "";
        private static string AuthPassword = "";
        private static string Bucket = "gs://asp-mvc-with-android.appspot.com";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}