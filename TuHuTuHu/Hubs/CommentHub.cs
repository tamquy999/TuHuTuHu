using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using TuHuTuHu.Models;

namespace TuHuTuHu.Hubs
{
    public class CommentHub : Hub
    {
        public void Comment(int postID, int userID, string content)
        {
            MyDBContext db = new MyDBContext();

            Comment cmt = new Comment();
            cmt.PostID = postID;
            cmt.UserID = userID;
            cmt.Content = content;

            db.Comment.Add(cmt);

            //db.SaveChanges();

            Account acc = db.Account.Where(s => s.AccID == userID).FirstOrDefault();

            Clients.All.addCommentToPost(postID, acc.AccID, acc.AvtLink, acc.Fullname, content);
        }
    }
}