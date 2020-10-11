using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuHuTuHu.Models;

namespace TuHuTuHu.Controllers
{
    public class PostController : Controller
    {
        MyDBContext db = new MyDBContext();
        Post thisPost = new Post();
        Account myAcc = new Account();

        public ActionResult Index(string postID)
        {
            myAcc = db.Account.Find(Convert.ToInt32(Session["userID"]));
            
            if (postID != "" && postID != null && myAcc != null)
            {
                thisPost = db.Post.Find(Convert.ToInt32(postID));
                return View(thisPost);
            }
            else
            {
                return null;
            }
            
        }
    }
}