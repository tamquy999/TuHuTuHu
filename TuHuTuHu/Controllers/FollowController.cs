using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuHuTuHu.Models;

namespace TuHuTuHu.Controllers
{
    [Authorize]
    public class FollowController : Controller
    {
        MyDBContext dbContext = new MyDBContext();
        Account acc = new Account();

        // GET: Follow
        public ActionResult Index()
        {
            return View();
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