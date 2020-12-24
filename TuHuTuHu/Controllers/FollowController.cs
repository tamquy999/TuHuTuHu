using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuHuTuHu.Models;

namespace TuHuTuHu.Controllers
{
    [Authorize]
    public class FollowController : BaseController
    {
        MyDBContext dbContext = new MyDBContext();
        Account acc = new Account();

        // GET: Follow
        public ActionResult Index()
        {
            acc = base.db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();

            ViewBag.CurrentUser = acc;
            ViewBag.Contacts = GetAllContact();

            FollowPage followpage = new FollowPage();

            followpage.account = acc;
            followpage.follower = dbContext.Follow.Where(s => s.UserID == acc.AccID).ToList();
            followpage.following = dbContext.Follow.Where(s => s.FollowerID == acc.AccID).ToList();

            followpage.countFollower = dbContext.Follow.Count(s => s.UserID == acc.AccID);
            followpage.countFollowing = dbContext.Follow.Count(s => s.FollowerID == acc.AccID);
            return View(followpage);
        }

        List<Account> GetAllContact()
        {
            List<Msg> messages = dbContext.Msg.Where(s => s.Account.AccID == acc.AccID || s.Account1.AccID == acc.AccID).ToList();
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
            List<Account> contacts = dbContext.Account.Where(s => contactIDs.Contains(s.AccID.ToString())).ToList();
            return contacts;
        }

        [HttpGet]
        public ActionResult ReverseFollowState(int myUserID, int theirID, int followID, string whereTab)
        {

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}