using UsersList.Models;

namespace UsersList.Utils
{
    public static class SaveData
    {
        public static void SaveUser(UserContext context, User user)
        {
            if (user.UserId == 0)
            {
                context.Users.Add(user);
            }
            else
            {
                User dbEntry = context.Users.Find(user.UserId);
                if (dbEntry != null)
                {
                    dbEntry.UserName = user.UserName;
                    dbEntry.UserEmail = user.UserEmail;
                    dbEntry.SkypeLogin = user.SkypeLogin;
                    dbEntry.Signature = user.Signature;
                    dbEntry.UserAvatar = user.UserAvatar;
                    dbEntry.ImageMimeType = user.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
    }
}