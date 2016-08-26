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
        public ActionResult Add(User user, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                //if (image != null)
                //{
                //    user.ImageMimeType = image.ContentType;
                //    user.UserAvatar = new byte[image.ContentLength];
                //    image.InputStream.Read(user.UserAvatar, 0, image.ContentLength);
                //}
                Utils.UtilsUser.UpdateUserByModelFronWeb(_db, ref user);
                //return RedirectToAction("Index");
            }
            //else
            //{
            //    return View(user);
            //}
            return Json(new { res = true});
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