using Messenger.DAL.Context;
using Messenger.DAL.Entities;
using Messenger.DAL.Repositories.Interfaces;

namespace Messenger.DAL.Repositories
{
    public class ChatsRepository : BaseRepository<Chat, int>, IChatsRepository
    {
        public ChatsRepository(AppDbContext context) : base(context)
        {

        }
    }
}
