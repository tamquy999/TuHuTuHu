using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuHuTuHu.Models;

namespace TuHuTuHu.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        MyDBContext dbContext = new MyDBContext();
        Post thisPost = new Post();
        Account acc = new Account();

        public ActionResult Index(string postID)
        {
            acc = dbContext.Account.Find(Convert.ToInt32(Session["userID"]));

            ViewBag.CurrentUser = acc;
            ViewBag.Contacts = GetAllContact();

            if (postID != "" && postID != null && acc != null)
            {
                thisPost = dbContext.Post.Find(Convert.ToInt32(postID));
                return View(thisPost);
            }
            else
            {
                return null;
            }
            
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
    }
}