using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuHuTuHu.Models
{
    public class Userpage
    {
        public Account account { get; set; }

        public List<Post> posts { get; set; }
    }
}