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
        Account ownerPost = new Account();
        Account myAcc = new Account();
        List<Comment> allComments;

        UserPost userPost = new UserPost();
        // GET: Post
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID">userID của người chủ post</param>
        /// <param name="postID">postID của bài viết cần xem</param>
        /// <returns></returns>
        public ActionResult Index(string userID, string postID)
        {
            myAcc = db.Account.Find(Convert.ToInt32(Session["userID"]));
            
            if (postID != "" && postID != null && userID != "" && userID != null)
            {
                ownerPost = db.Account.Find(Convert.ToInt32(userID));
                thisPost = db.Post.Find(Convert.ToInt32(postID));
                allComments = userPost.GetAllCommentPerPost(db, postID);

                userPost.myAcc = this.myAcc;
                userPost.ownerPost = this.ownerPost;
                userPost.thisPost = this.thisPost;
                userPost.allComments = this.allComments;

                return View(userPost);
            }
            else
            {
                return null;
            }
            
        }
    }
}