using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuHuTuHu.Models;

namespace TuHuTuHu.Controllers
{
    [Authorize]
    public class NewfeedController : BaseController
    {
        int postPerClick = 2;

        // GET: Newfeed
        public ActionResult Index()
        {
            Session["postCount"] = 0;

            acc = base.db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();

            var posts = base.GetAllRelatePosts();

            ViewBag.Chat = null;
            ViewBag.CurrentUser = base.acc;
            ViewBag.Contacts = GetAllContact();     

            Newfeed newfeed = new Newfeed();
            newfeed.account = base.acc;
            newfeed.allPosts = posts;

            return View(posts);
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, string yourMind)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    var filename = DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), filename);
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";

                    acc = db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();

                    Post post = new Post();
                    post.CreatedAt = DateTime.Now;
                    post.Content = yourMind;
                    post.ImgLink = "/UploadedFiles/" + filename;
                    post.UserID = acc.AccID;

                    db.Post.Add(post);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return RedirectToAction("Index");
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