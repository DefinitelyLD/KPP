using Messenger.DAL.Context;
using Messenger.DAL.Entities;
using Messenger.DAL.Repositories.Interfaces;

namespace Messenger.DAL.Repositories
{
    public class MessageImagesRepository : BaseRepository<MessageImage, int>, IMessageImagesRepository
    {
        public MessageImagesRepository(AppDbContext context) : base(context)
        {

        }
    }
}
