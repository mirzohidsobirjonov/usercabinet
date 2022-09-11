using Microsoft.EntityFrameworkCore;
using UserCabinet.Domain.Entities.Attachments;
using UserCabinet.Domain.Entities.Users;

namespace UserCabinet.Data.DbContexts
{
    public class UserCabinetDbContext : DbContext
    {
        public UserCabinetDbContext(DbContextOptions<UserCabinetDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Attechment> Attechments { get; set; }
    }
}
