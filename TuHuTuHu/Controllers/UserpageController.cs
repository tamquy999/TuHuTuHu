using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuHuTuHu.Models;

namespace TuHuTuHu.Controllers
{
    [Authorize]
    public class UserpageController : BaseController
    {

        // GET: Userpage
        public ActionResult Index(int id)
        {
            acc = db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();

            ViewBag.CurrentUser = acc;
            ViewBag.Contacts = GetAllContact();

            Userpage userpage = new Userpage();

            userpage.account = db.Account.Find(id);
            userpage.posts = db.Post.Where(s => s.UserID == id).ToList();

            return View(userpage);
        }

        List<Account> GetAllContact()
        {
            List<Msg> messages = db.Msg.Where(s => s.Account.AccID == acc.AccID || s.Account1.AccID == acc.AccID).ToList();
            List<string> contactIDs = new List<string>();
            foreach (var message in messages)
            {
                // Neu minh la nguoi gui thi lay id nguoi nhan va nguoc lai
                if (message.Account.AccID == acc.AccID)
                {
                    contactIDs.Add(message.Account1.AccID.ToString());
                }
                else contactIDs.Add(message.Account.AccID.ToString());
            }
            List<Account> contacts = db.Account.Where(s => contactIDs.Contains(s.AccID.ToString())).ToList();
            return contacts;
        }

        public ActionResult EditUserInfo(string fullname, string newPwd)
        {
            acc = db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();
            if (fullname != "")
            {
                acc.Fullname = fullname;
            }
            if (newPwd != "")
            {
                acc.Pass = HashPass(newPwd);
            }

            db.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}