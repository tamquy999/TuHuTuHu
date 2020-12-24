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
            ViewBag.FollowedTheir = CheckMeFollowFriendReturnFolowID(acc,id);

            (ViewBag.countFollower, ViewBag.countFollowed) = CountFollow(id);

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

        public int CheckMeFollowFriendReturnFolowID(Account myAcc,int id)
        {
            List<Follow> temp = db.Follow.Where(s => s.FollowerID == myAcc.AccID && s.UserID == id).ToList();

            if(temp.Count != 0)
            {
                return temp[0].FollowID; //da follow ngta roi
            }
            else
                return -1; //chua follow ngta
        }

        Tuple<int,int> CountFollow(int id)
        {
            acc = db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();
            int countFollower = db.Follow.Count(s => s.UserID == id);
            int countFollowed = db.Follow.Count(s => s.FollowerID == id);
            return Tuple.Create(countFollower, countFollowed);
        }

        [HttpGet]
        public ActionResult ReverseFollowingStateUserPage(int myUserID, int theirID, int followID)
        {
            acc = base.db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();

            if (acc.AccID == myUserID)
            {
                List<Follow> thisFollowRow = base.db.Follow.Where(s => s.FollowerID == myUserID && s.UserID == theirID).ToList();

                if (thisFollowRow.Count == 0)
                {
                    Follow temp = new Follow();
                    temp.UserID = theirID;
                    temp.FollowerID = myUserID;
                    base.db.Follow.Add(temp);
                    db.SaveChanges();
                }
                else
                {
                    foreach (var temp in thisFollowRow)
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