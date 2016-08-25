using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsersList.Models;

namespace UsersList.Controllers
{
    public class UserController : Controller
    {
        UserContext db = new UserContext();

        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult Add(int? userId)
        {
            return View(userId != null ? db.Users.Find(userId) : null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(string userId, HttpPostedFileBase image)
        {
            if (ModelState.IsValid && user != null)
            {
                var user = userId != null ? db.Users.Find(userId) : null;

                if (user != null && image != null)
                {
                    user.ImageMimeType = image.ContentType;
                    user.UserAvatar = new byte[image.ContentLength];
                    image.InputStream.Read(user.UserAvatar, 0, image.ContentLength);
                }
                Utils.SaveData.SaveUser(db, user);
                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
        }

        public FileContentResult GetImage(int userId)
        {
            User userImage = db.Users.FirstOrDefault(p => p.UserId == userId);
            return userImage != null ? File(userImage.UserAvatar, userImage.ImageMimeType) : null;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}