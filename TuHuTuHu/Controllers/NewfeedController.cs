using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuHuTuHu.Models;

namespace TuHuTuHu.Controllers
{
    public class NewfeedController : Controller
    {

        MyDBContext dbContext = new MyDBContext();
        Account acc;
        List<Post> posts;

        // GET: Newfeed
        public ActionResult Index()
        {
            acc = dbContext.Account.Find(Convert.ToInt32(Session["userID"]));

            posts = dbContext.Post.ToList();

            Newfeed newfeed = new Newfeed();
            newfeed.account = acc;
            newfeed.allPosts = posts;

            return View(newfeed);
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(file.FileName));
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View();
        }
    }
}