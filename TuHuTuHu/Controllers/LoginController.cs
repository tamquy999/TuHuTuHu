using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuHuTuHu.Models;

namespace TuHuTuHu.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            Account acc = CheckCookies();
            if (acc != null)
            {
                Session["userID"] = acc.AccID.ToString().Trim();
                return RedirectToAction("Index", "Newfeed");
            }
            return View();
        }



        [HttpPost]
        public ActionResult LoginResult(string user, string pass)
        {
            MyDBContext dbContext = new MyDBContext();
            Account acc = dbContext.Account.Where(s => s.Username == user && s.Pass == pass).FirstOrDefault<Account>();
            if (acc != null)
            {
                Session["userID"] = acc.AccID.ToString().Trim();
                HttpCookie accInfo = new HttpCookie("accInfo");
                accInfo["userID"] = acc.AccID.ToString().Trim();
                accInfo.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(accInfo);
                return RedirectToAction("Index", "Newfeed");
            }
            else return View("Index");
        }

        [HttpPost]
        public ActionResult LogoutResult()
        {
            HttpCookie accInfo = new HttpCookie("accInfo");
            accInfo.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(accInfo);
            return RedirectToAction("Index", "Login");
        }

        public Account CheckCookies()
        {
            Account acc = null;
            HttpCookie reqCookies = Request.Cookies["accInfo"];
            if (reqCookies != null)
            {
                acc = new Account();
                acc.AccID = Convert.ToInt32(reqCookies["userID"].ToString());
            }
            return acc;
        }
    }
}