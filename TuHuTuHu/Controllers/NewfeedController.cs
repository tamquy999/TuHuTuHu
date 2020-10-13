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
        Account acc = new Account();
        int postPerClick = 2;


        // GET: Newfeed
        public ActionResult Index()
        {
            Session["postCount"] = 0;

            acc = dbContext.Account.Find(Convert.ToInt32(Session["userID"]));

            var posts = GetAllRelatePosts();

            ViewBag.CurrentUser = acc;
            ViewBag.Contacts = GetAllContact();

            Newfeed newfeed = new Newfeed();
            newfeed.account = acc;
            newfeed.allPosts = posts;

            return View(posts);
        }

        List<Post> GetAllRelatePosts()
        {
            List<Follow> followers = dbContext.Follow.Where(s => s.FollowerID == acc.AccID).ToList();
            List<string> followerIDs = new List<string>();
            foreach (var follower in followers)
            {
                followerIDs.Add(follower.UserID.ToString());
            }
            List<Post> posts = dbContext.Post.Where(s => followerIDs.Contains(s.UserID.ToString()) || s.UserID == acc.AccID).ToList();
            return posts;
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

        public PartialViewResult GetPostData()
        {
            System.Threading.Thread.Sleep(2000);
            var posts = GetAllRelatePosts();
            int postCount = Convert.ToInt32(Session["postCount"]);
            IEnumerable<Post> result;
            if (postCount < posts.Count())
            {
                result = posts.Skip(postCount).Take(postPerClick);
                postCount += postPerClick;
                Session["postCount"] = postCount;
            }
            else result = null;
            Newfeed nf = new Newfeed();
            nf.allPosts = result.ToList();
            return PartialView("_Post", nf);
        }
    }
}