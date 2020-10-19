using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TuHuTuHu.Models;

namespace TuHuTuHu.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string user, string pass)
        {
            using (MyDBContext context = new MyDBContext())
            {
                ViewBag.LoginMessage = null;
                bool IsValidUser = context.Account.Any(s => s.Username.ToLower() ==
                     user && s.Pass == pass);
                if (IsValidUser)
                {
                    var acc = context.Account.Where(s => s.Username == user).FirstOrDefault();
                    FormsAuthentication.SetAuthCookie(acc.Username, true);
                    return RedirectToAction("Index", "Newfeed");
                }
                ViewBag.LoginMessage = "Tài khoản hoặc mật khẩu sai";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}