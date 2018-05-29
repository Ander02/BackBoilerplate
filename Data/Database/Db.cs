using Data.Domain;
using Data.Domain.Identitty;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Database
{
    public class Db : DbContext
    {
        #region DbSets
        //Identity DbSets
        public DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> User_Roles { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }

        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        #endregion

        public Db() { }

        public Db(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Tables
            modelBuilder.Entity<RoleClaim>().ToTable(nameof(RoleClaims));
            modelBuilder.Entity<Role>().ToTable(nameof(Roles));
            modelBuilder.Entity<User_Role>().ToTable(nameof(User_Roles));
            modelBuilder.Entity<UserClaim>().ToTable(nameof(UserClaims));

            modelBuilder.Entity<User>().ToTable(nameof(Users));
            modelBuilder.Entity<UserLogin>().ToTable(nameof(UserLogin));
            modelBuilder.Entity<Task>().ToTable(nameof(Tasks));

            //RelashionShips
            modelBuilder.Entity<UserLogin>().HasKey(ul => new { ul.LoginProvider, ul.ProviderKey }).ForSqlServerIsClustered(true);
            modelBuilder.Entity<User_Role>().HasKey(ur => new { ur.UserId, ur.RoleId }).ForSqlServerIsClustered(true);

            modelBuilder.Entity<Task>().HasOne(t => t.User).WithMany(u => u.Tasks);
        }
    }
}
