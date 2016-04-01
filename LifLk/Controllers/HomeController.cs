using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifLk.Models;

namespace LifLk.Controllers
{
    public class HomeController : Controller
    {
        LifDB db = new LifDB();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Message = "Your application description page.";
                if (Session["userSteamId"] == null)
                {
                    HttpContext.GetOwinContext().Authentication.SignOut();
                    return RedirectToAction("Index", "Home");
                }
                decimal steamId = decimal.Parse(Session["userSteamId"].ToString());
                account acc = db.account.Where(p => p.SteamID == steamId).FirstOrDefault();

                return View(acc);
            }
            else
            {
                return View();
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}