using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuHuTuHu.Models
{
    public class ResultSearch
    {
        public Account account { get; set; }

        public List<Account> resultAccounts { get; set; }
    }
}