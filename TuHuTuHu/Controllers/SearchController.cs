using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuHuTuHu.Models;

namespace TuHuTuHu.Controllers
{
    public class SearchController : Controller
    {
        MyDBContext dbContext = new MyDBContext();
        Account acc;
        List<Account> resultAccounts;


        // GET: Search
        public ActionResult Index(string searchString)
        {
            acc = dbContext.Account.Find(Convert.ToInt32(Session["userID"]));

            
            if (searchString == null)
            {
                return null;
            }
            else
            {
                resultAccounts = dbContext.Account.Where(x => x.Username.Contains(searchString)).ToList();

                ResultSearch result = new ResultSearch();
                result.account = acc;
                result.resultAccounts = resultAccounts;
                return View(result);
            }
        }
    }
}