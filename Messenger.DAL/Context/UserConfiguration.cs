using Messenger.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.DAL.Context
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            //user user friends
            modelBuilder.HasMany(c => c.FriendsTo)
                .WithMany(s => s.FriendsFrom)
                .UsingEntity(j => j.ToTable("UserUserFriends"));

            //user user blocked
            modelBuilder.HasMany(c => c.BlockedUsersTo)
                .WithMany(s => s.BlockedUsersFrom)
                .UsingEntity(j => j.ToTable("UserUserBlocked"));
        }
    }
}
