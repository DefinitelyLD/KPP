using Messenger.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Messenger.DAL.Context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        #region DbSets

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<MessageImage> MessageImages { get; set; }

        public DbSet<ChatImage> ChatImages { get; set; }

        public DbSet<UserAccount> UserAccounts { get; set; }

        public DbSet<UserImage> UserImages { get; set; }

        public DbSet<ActionLog> ActionLogs { get; set; }

        #endregion

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
        }
    }
}
