using Messenger.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DAL.Context
{
    public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> modelBuilder)
        {
            // chat user
            modelBuilder.HasKey(t => new { t.ChatId, t.UserId });

            modelBuilder.HasOne(pt => pt.Chat)
                .WithMany(t => t.Users)
                .HasForeignKey(pt => pt.ChatId);

            modelBuilder.HasOne(pt => pt.User)
                .WithMany(p => p.Chats)
                .HasForeignKey(pt => pt.UserId);
            // /chat user/
        }
    }
}
