using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuHuTuHu.Models;

namespace TuHuTuHu.Controllers
{
    public class NewfeedController : Controller
    {

        MyDBContext dbContext = new MyDBContext();
        

        // GET: Newfeed
        public ActionResult Index()
        {
            Account acc = dbContext.Account.Find(Convert.ToInt32(Session["userID"]));
            return View(acc);
        }
    }
}