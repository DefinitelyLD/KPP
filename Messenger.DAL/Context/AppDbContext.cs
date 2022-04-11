using Messenger.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageImage> MessageImages { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // chat user
            modelBuilder.Entity<ChatUser>()
            .HasKey(t => new { t.ChatId, t.UserId });

            modelBuilder.Entity<ChatUser>()
                .HasOne(pt => pt.Chat)
                .WithMany(t => t.ChatUsers)
                .HasForeignKey(pt => pt.ChatId);

            modelBuilder.Entity<ChatUser>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.ChatUsers)
                .HasForeignKey(pt => pt.UserId);
            // /chat user/

            //user user friends
            modelBuilder.Entity<User>()
                .HasMany(c => c.FriendsTo)
                .WithMany(s => s.FriendsFrom)
                .UsingEntity(j => j.ToTable("UserUserFriends"));

            //user user blocked
            modelBuilder.Entity<User>()
                .HasMany(c => c.BlockedUsersTo)
                .WithMany(s => s.BlockedUsersFrom)
                .UsingEntity(j => j.ToTable("UserUserBlocked"));
        }
    }
}
