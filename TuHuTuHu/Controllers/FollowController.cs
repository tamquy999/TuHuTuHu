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
        
        Account acc = new Account();

        // GET: Follow
        public ActionResult Index()
        {
            acc = base.db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();

            ViewBag.CurrentUser = acc;
            ViewBag.Contacts = GetAllContact();

            FollowPage followpage = new FollowPage();

            followpage.account = acc;
            followpage.follower = db.Follow.Where(s => s.UserID == acc.AccID).ToList();
            followpage.following = db.Follow.Where(s => s.FollowerID == acc.AccID).ToList();

            followpage.countFollower = db.Follow.Count(s => s.UserID == acc.AccID);
            followpage.countFollowing = db.Follow.Count(s => s.FollowerID == acc.AccID);
            return View(followpage);
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

        [HttpGet]
        public ActionResult ReverseFollowingState(int myUserID, int theirID, int followID)
        {
            acc = base.db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();

            if (acc.AccID == myUserID)
            {
                List<Follow> thisFollowRow = base.db.Follow.Where(s => s.FollowerID == myUserID && s.UserID == theirID).ToList();

                if(thisFollowRow.Count == 0)
                {
                    Follow temp = new Follow();
                    temp.UserID = theirID;
                    temp.FollowerID = myUserID;
                    base.db.Follow.Add(temp);
                    db.SaveChanges();
                }
                else
                {
                    foreach(var temp in thisFollowRow)
                    {
                        base.db.Follow.Remove(temp);
                        db.SaveChanges();
                    }
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }



    }
}