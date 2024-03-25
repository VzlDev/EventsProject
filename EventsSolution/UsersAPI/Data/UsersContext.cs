using Microsoft.EntityFrameworkCore;
using UsersAPI.Model;

namespace UsersAPI.Data
{
    public partial class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions
        <UsersContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(k => k.UserId);
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
