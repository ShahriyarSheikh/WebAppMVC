using Microsoft.EntityFrameworkCore;
using WebAppData.Models;

namespace WebAppData
{
    public class DatabaseContext :DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<UserSessions> UserSession { get; set; }

    }
}
