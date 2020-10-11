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
        public Account ownerPost { get; set; }

        public Post thisPost { get; set; }

        public Account myAcc { get; set; }

        public List<Comment> allComments { get; set; }
    }
}