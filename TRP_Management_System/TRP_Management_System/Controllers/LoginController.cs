using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRP_Management_System.DTOs;
using TRP_Management_System.EF;

namespace TRP_Management_System.Controllers
{
    public class LoginController : Controller
    {
        TRP_DBEntities1 db = new TRP_DBEntities1();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginDTO log)
        {
            var user = (from u in db.Users
                        where u.Username.Equals(log.Username) &&
                        u.Password.Equals(log.Password)
                        select u).SingleOrDefault();
            if (user != null)
            {
                Session["user"] = user;
                return RedirectToAction("List", "Program");
            }
            return View();
        }
    }
}