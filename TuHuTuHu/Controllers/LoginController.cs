using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TuHuTuHu.Models;

namespace TuHuTuHu.Controllers
{
    public class LoginController : BaseController
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
                pass = HashPass(pass);
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


        [HttpPost]
        public ActionResult Signup(string fullname, string user, string pass)
        {
            using (MyDBContext context = new MyDBContext())
            {
                ViewBag.SignupMessage = null;

                if (context.Account.Where(s => s.Username == user).FirstOrDefault() == null)
                {
                    Account acc = new Account();
                    acc.Username = user;
                    acc.Fullname = fullname;
                    acc.Pass = pass;
                    acc.JoinDate = DateTime.Now;
                    acc.AvtLink = "/Content/images/avatar.png";
                    acc.CoverLink = "/Content/images/usrcover.jpg";

                    context.Account.Add(acc);
                    context.SaveChanges();
                    ViewBag.SignupMessage = "Đăng ký thành công.";

                    FormsAuthentication.SetAuthCookie(acc.Username, true);
                    return RedirectToAction("Index", "Newfeed");
                }

                ViewBag.SignupMessage = "Đăng ký không thành công. Vui lòng thử lại.";
                return View();
            }
        }

        [HttpPost]
        public ActionResult ForgetPass(string user, string newpass1, string newpass2)
        {
            using(MyDBContext context = new MyDBContext())
            {
                ViewBag.ForgetMessage = null;
                Account acc = context.Account.Where(s => s.Username == user).FirstOrDefault();

                if (acc != null && newpass1 == newpass2)
                {
                    acc.Pass = HashPass(newpass1);
                    context.SaveChanges();
                    ViewBag.SignupMessage = "Đổi mật khẩu thành công.";
                    FormsAuthentication.SetAuthCookie(acc.Username, true);
                    return RedirectToAction("Index", "Newfeed");
                }

                ViewBag.ForgetMessage = "Quên mật khẩu không thành công. Vui lòng thử lại.";
                return View();
            }
        }
    }
}