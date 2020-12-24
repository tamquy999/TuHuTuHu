using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TuHuTuHu.Models;

namespace TuHuTuHu.Controllers
{
    public class BaseController : Controller
    {
        protected MyDBContext db = new MyDBContext();

        protected Account acc { get; set; }

        protected List<Post> GetAllRelatePosts()
        {
            acc = db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();
            List<Follow> followers = db.Follow.Where(s => s.FollowerID == acc.AccID).ToList();
            List<string> followerIDs = new List<string>();
            foreach (var follower in followers)
            {
                followerIDs.Add(follower.UserID.ToString());
            }
            List<Post> posts = db.Post.Where(s => followerIDs.Contains(s.UserID.ToString()) || s.UserID == acc.AccID).ToList();
            posts.Reverse();
            return posts;

        }

        protected List<Account> GetAllContact()
        {
            List<Msg> messages = db.Msg.Where(s => s.Account.AccID == acc.AccID || s.Account1.AccID == acc.AccID).ToList();
            List<string> contactIDs = new List<string>();
            foreach (var message in messages)
            {
                // Neu minh la nguoi gui thi lay id nguoi nhan va nguoc lai
                if (message.Account.AccID == acc.AccID)
                {
                    contactIDs.Add(message.Account1.AccID.ToString());
                }
                else contactIDs.Add(message.Account.AccID.ToString());
            }
            List<Account> contacts = db.Account.Where(s => contactIDs.Contains(s.AccID.ToString())).ToList();
            return contacts;
        }

        //[HttpPost]
        public ActionResult LoadConversation(int accID)
        {
            Account account = db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();
            ViewBag.CurrentUser = account;
            //List<Msg> messages = db.Msg.Where(s => s.SenderID == account.AccID).ToList();
            List<Msg> messages = db.Msg.Where(s => (s.SenderID == accID && s.ReceiverID == account.AccID) || (s.ReceiverID == accID && s.SenderID == account.AccID)).ToList();

            ViewBag.Chat = messages;

            return PartialView("_ChatBody", "Shared");
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, string yourMind)
        {
            string imgLink = "NULL";
            if (file != null && file.ContentLength > 0)
                try
                {
                    var filename = DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), filename);
                    if (!Directory.Exists(Server.MapPath("~/UploadedFiles")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/UploadedFiles"));
                    }
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";

                    imgLink = "/UploadedFiles/" + filename;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            acc = db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();

            Post post = new Post();
            post.CreatedAt = DateTime.Now;
            post.Content = yourMind;
            post.ImgLink = imgLink;
            post.UserID = acc.AccID;

            db.Post.Add(post);
            db.SaveChanges();

            return RedirectToAction("Index", "Newfeed");
        }

        public ActionResult LoveClick(int postID)
        {
            acc = db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();
            Love love = new Love();
            love.PostID = postID;
            love.UserID = acc.AccID;
            db.Love.Add(love);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoveUnClick(int postID)
        {
            acc = db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();
            Love love = db.Love.Where(s => s.PostID == postID && s.UserID == acc.AccID).FirstOrDefault();
            db.Love.Remove(love);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoveCheck(int postID)
        {
            acc = db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();
            Love love = db.Love.Where(s => s.PostID == postID && s.UserID == acc.AccID).FirstOrDefault();
            if (love != null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PassCheck(string pwd)
        {
            acc = db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();
            if (acc.Pass.Trim() == HashPass(pwd))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public string HashPass(string pass)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(pass);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                //return sb.ToString();
                return pass;
            }
        }
    }
}