using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using TuHuTuHu.Models;

namespace TuHuTuHu.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string userID, string message, string receiverID)
        {
            MyDBContext db = new MyDBContext();

            Msg msg = new Msg();
            msg.SenderID = Convert.ToInt32(userID);
            msg.ReceiverID = Convert.ToInt32(receiverID);
            msg.Content = message;
            msg.CreatedAt = DateTime.Now;

            db.Msg.Add(msg);

            db.SaveChanges();

            var acc = db.Account.Where(s => s.Username == Context.User.Identity.Name).FirstOrDefault();

            Clients.User(receiverID).addYourMessageToPage(message, acc.AvtLink);
            Clients.User(userID).addMyMessageToPage(message);
        }
    }
}