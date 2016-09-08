using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsersList.Models;

namespace UsersList.Controllers
{
    public class UserController : Controller
    {
        private readonly UserContext _db = new UserContext();

        public ActionResult Index()
        {
            return View(_db.Users.ToList());
        }

        public ActionResult Add(int? userId)
        {
            return View(userId != null ? _db.Users.Find(userId) : null);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Add()
        {
            User userFromDb;

            if (!ModelState.IsValid) return Json(new {res = false});

            HttpPostedFileBase image = Request.Files["userAvatar"];
                
            var userIdWeb = Request.Form[0];

            if (userIdWeb == "")
            {
                userFromDb = new User
                {
                    UserName = Request.Form["userName"],
                    UserEmail = Request.Form["userEmail"],
                    SkypeLogin = Request.Form["skypeLogin"],
                    Signature = Request.Form["signature"]
                };

                if (image != null)
                {
                    userFromDb.ImageMimeType = image.ContentType;
                    userFromDb.UserAvatar = new byte[image.ContentLength];
                    image.InputStream.Read(userFromDb.UserAvatar, 0, image.ContentLength);
                }
            }
            else
            {
                userFromDb = _db.Users.Find(int.Parse(userIdWeb));

                userFromDb.UserName = Request.Form["userName"];
                userFromDb.UserEmail = Request.Form["userEmail"];
                userFromDb.SkypeLogin = Request.Form["skypeLogin"];
                userFromDb.Signature = Request.Form["signature"];

                if (image != null)
                {
                    userFromDb.ImageMimeType = image.ContentType;
                    userFromDb.UserAvatar = new byte[image.ContentLength];
                    image.InputStream.Read(userFromDb.UserAvatar, 0, image.ContentLength);
                }
            }

            Utils.UtilsUser.UpdateUserByModelFronWeb(_db, ref userFromDb);

            return Json(new {res = true});
        }
        
        public FileContentResult GetImage(int userId)
        {
            User userImage = _db.Users.FirstOrDefault(p => p.UserId == userId);
            return userImage != null ? File(userImage.UserAvatar, userImage.ImageMimeType) : null;
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}