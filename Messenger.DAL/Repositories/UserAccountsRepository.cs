using Messenger.DAL.Context;
using Messenger.DAL.Entities;
using Messenger.DAL.Repositories.Interfaces;

namespace Messenger.DAL.Repositories
{
    public class UserAccountsRepository : BaseRepository<UserAccount, int>, IUserAccountsRepository
    {
        public UserAccountsRepository(AppDbContext context) : base(context)
        {
        }
    }
}
