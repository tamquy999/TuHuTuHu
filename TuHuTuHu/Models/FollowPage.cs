using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuHuTuHu.Models
{
    public class FollowPage
    {
        public Account account { get; set; }

        public List<Follow> following { get; set; }

        public List<Follow> follower { get; set; }

        public int countFollowing { get; set; }

        public int countFollower { get; set; }
    }
}