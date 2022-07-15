using Messenger.DAL.Context;
using Messenger.DAL.Entities;
using Messenger.DAL.Repositories.Interfaces;

namespace Messenger.DAL.Repositories
{
    public class UsersRepository : BaseRepository<User, string>, IUsersRepository
    {
        public UsersRepository(AppDbContext context) : base(context)
        {

        }
    }
}
