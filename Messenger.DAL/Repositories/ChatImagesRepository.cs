using Messenger.DAL.Context;
using Messenger.DAL.Entities;
using Messenger.DAL.Repositories.Interfaces;

namespace Messenger.DAL.Repositories
{
    public class ChatImagesRepository : BaseRepository<ChatImage, int>, IChatImagesRepository
    {
        public ChatImagesRepository(AppDbContext context) : base(context)
        {

        }
    }
}
