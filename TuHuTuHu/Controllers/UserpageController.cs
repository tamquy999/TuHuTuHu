using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuHuTuHu.Models;

namespace TuHuTuHu.Controllers
{
    [Authorize]
    public class UserpageController : BaseController
    {

        // GET: Userpage
        public ActionResult Index(int id)
        {
            acc = db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();

            ViewBag.CurrentUser = acc;
            ViewBag.Contacts = GetAllContact();

            Userpage userpage = new Userpage();

            userpage.account = db.Account.Find(id);
            userpage.posts = db.Post.Where(s => s.UserID == id).ToList();

            return View(userpage);
        }

        List<Account> GetAllContact()
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

        public ActionResult EditUserInfo(string fullname, string newPwd, HttpPostedFileBase newAvt, HttpPostedFileBase newCover)
        {
            acc = db.Account.Where(s => s.Username == User.Identity.Name).FirstOrDefault();

            // check ten
            if (fullname != "")
            {
                acc.Fullname = fullname;
            }

            // check pass
            if (newPwd != "")
            {
                acc.Pass = HashPass(newPwd);
            }

            // check avt
            string avtLink = "NULL";
            if (newAvt != null && newAvt.ContentLength > 0)
            {
                try
                {
                    var filename = DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(newAvt.FileName);
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), filename);
                    if (!Directory.Exists(Server.MapPath("~/UploadedFiles")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/UploadedFiles"));
                    }
                    newAvt.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";

                    avtLink = "/UploadedFiles/" + filename;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }

                acc.AvtLink = avtLink;
            }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            // check cover
            string coverLink = "NULL";
            if (newCover != null && newCover.ContentLength > 0)
            {
                try
                {
                    var filename = DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(newCover.FileName);
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), filename);
                    if (!Directory.Exists(Server.MapPath("~/UploadedFiles")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/UploadedFiles"));
                    }
                    newCover.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";

                    coverLink = "/UploadedFiles/" + filename;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }

                acc.CoverLink = coverLink;
            }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            db.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}