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
            ViewBag.Contacts = base.GetAllContact();     

            Newfeed newfeed = new Newfeed();
            newfeed.account = base.acc;
            newfeed.allPosts = posts;

            return View(posts);
        }

        public ActionResult DeletePost(string selectedPost)
        {
            var comments = db.Comment.Where(s => s.PostID.ToString() == selectedPost).ToList();
            foreach (var cmt in comments)
            {
                db.Comment.Remove(cmt);
            }
            var loves = db.Love.Where(s => s.PostID.ToString() == selectedPost).ToList();
            foreach (var love in loves)
            {
                db.Love.Remove(love);
            }
            var post = db.Post.Find(Convert.ToInt32(selectedPost));
            db.Post.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index", "Newfeed");
        }

        public ActionResult EditPost(string selectedPost, string yourMind)
        {
            var post = db.Post.Find(Convert.ToInt32(selectedPost));
            post.Content = yourMind;
            db.SaveChanges();
            //return RedirectToAction("Index", "Newfeed");
            return Redirect(Request.UrlReferrer.ToString());
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