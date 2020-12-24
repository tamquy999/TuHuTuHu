using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuHuTuHu.Models;


namespace TuHuTuHu.Controllers
{
    [Authorize]
    public class SearchController : BaseController
    {
        Account acc;

        // GET: Search
        public ActionResult Index(string searchString)
        {
            acc = db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();




            ViewBag.CurrentUser = acc;
            ViewBag.Contacts = GetAllContact();

            ResultSearch result = new ResultSearch();

            result.account = acc;


            return View();
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

        [HttpPost]
        public ActionResult SearchResult(string searchbar)
        {
            return RedirectToAction("Index", new { searchString = searchbar });
        }
    }
}