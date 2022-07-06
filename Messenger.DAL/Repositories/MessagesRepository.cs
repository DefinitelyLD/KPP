using Messenger.DAL.Context;
using Messenger.DAL.Entities;
using Messenger.DAL.Repositories.Interfaces;

namespace Messenger.DAL.Repositories
{
    public class MessagesRepository : BaseRepository<Message, int>, IMessagesRepository
    {
        public MessagesRepository(AppDbContext context) : base(context)
        {

        }
    }
}
