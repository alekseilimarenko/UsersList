using System.Data.Entity;

namespace UsersList.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}