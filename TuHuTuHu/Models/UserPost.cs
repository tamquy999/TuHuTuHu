using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuHuTuHu.Models
{
    /// <summary>
    /// This post use to view 1 post in page Post
    /// 
    /// </summary>
    public class UserPost
    {
        public MyDBContext db = new MyDBContext();

        public Account ownerPost { get; set; }

        public Post thisPost { get; set; }

        public Account myAcc { get; set; }

        public List<Comment> allComments { get; set; }

        public List<Comment> GetAllCommentPerPost( MyDBContext db, string postID)
        {
            List<Comment> allCmts =new List<Comment>();

            allCmts = db.Comment.Where(c => c.PostID.ToString() == postID).ToList();

            return allCmts; 
        }

        public Account GetAccountByUserID(string userID)
        {
            Account temp = new Account();

            temp = this.db.Account.Find(Convert.ToInt32(userID));

            return temp;
        }

        public string CountLovedPost()
        {
            string temp = "";

            

            return temp;
        }
    }
}