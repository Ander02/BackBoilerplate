using Data.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Database
{
    public class Db : DbContext
    {
        #region DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        #endregion

        public Db() { }

        public Db(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(nameof(Users));
            modelBuilder.Entity<Task>().ToTable(nameof(Tasks)).HasOne(t => t.User).WithMany(u => u.Tasks);
        }
    }
}
