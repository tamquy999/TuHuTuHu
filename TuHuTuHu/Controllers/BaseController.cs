using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuHuTuHu.Models;

namespace TuHuTuHu.Controllers
{
    public class BaseController : Controller
    {
        protected MyDBContext db = new MyDBContext();

        protected Account acc { get; set; }

        protected List<Post> GetAllRelatePosts()
        {
            List<Follow> followers = db.Follow.Where(s => s.FollowerID == acc.AccID).ToList();
            List<string> followerIDs = new List<string>();
            foreach (var follower in followers)
            {
                followerIDs.Add(follower.UserID.ToString());
            }
            List<Post> posts = db.Post.Where(s => followerIDs.Contains(s.UserID.ToString()) || s.UserID == acc.AccID).ToList();
            posts.Reverse();
            return posts;

        }

        protected List<Account> GetAllContact()
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

        //[HttpPost]
        public ActionResult LoadConversation(int accID)
        {
            Account account = db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();
            ViewBag.CurrentUser = account;
            //List<Msg> messages = db.Msg.Where(s => s.SenderID == account.AccID).ToList();
            List<Msg> messages = db.Msg.Where(s => (s.SenderID == accID && s.ReceiverID == account.AccID) || (s.ReceiverID == accID && s.SenderID == account.AccID)).ToList();

            ViewBag.Chat = messages;

            return PartialView("_ChatBody", "Shared");
        }
    }
}