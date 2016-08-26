using UsersList.Models;

namespace UsersList.Utils
{
    public static class UtilsUser
    {
        public static void UpdateUserByModelFronWeb(UserContext context, ref User user)
        {
            if (user.UserId == 0)
            {
                context.Users.Add(user);
            }
            else
            {
                User userFromDb = context.Users.Find(user.UserId);
                if (userFromDb != null)
                {
                    userFromDb.UserName = user.UserName;
                    userFromDb.UserEmail = user.UserEmail;
                    userFromDb.SkypeLogin = user.SkypeLogin;
                    userFromDb.Signature = user.Signature;
                    if (user.UserAvatar != null)
                    {
                        userFromDb.UserAvatar = user.UserAvatar;
                        userFromDb.ImageMimeType = user.ImageMimeType;
                    }
                }
            }
            context.SaveChanges();
        }
    }
}