using Messenger.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DAL.Context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageImage> MessageImages { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
            modelBuilder.Entity<Chat>().Navigation(u => u.Messages).AutoInclude();
            modelBuilder.Entity<Chat>().Navigation(u => u.Users).AutoInclude();

            modelBuilder.Entity<MessageImage>().Navigation(u => u.Message).AutoInclude();

            modelBuilder.Entity<User>().Navigation(u => u.FriendsTo).AutoInclude();
            modelBuilder.Entity<User>().Navigation(u => u.FriendsFrom).AutoInclude();
            modelBuilder.Entity<User>().Navigation(u => u.BlockedUsersTo).AutoInclude();
            modelBuilder.Entity<User>().Navigation(u => u.BlockedUsersFrom).AutoInclude();
            modelBuilder.Entity<User>().Navigation(u => u.Messages).AutoInclude();

            modelBuilder.Entity<UserAccount>().Navigation(u => u.Chat).AutoInclude();
        }
    }
}
