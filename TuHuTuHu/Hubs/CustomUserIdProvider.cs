using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TuHuTuHu.Models;

namespace TuHuTuHu.Hubs
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            using (var db = new MyDBContext())
            {
                var acc = db.Account.Where(s => s.Username == request.User.Identity.Name).FirstOrDefault();
                return acc.AccID.ToString();
            }
        }
    }
}