using Messenger.DAL.Entities;

namespace Messenger.DAL.Repositories.Interfaces
{
    public interface IMessagesRepository : IRepository<Message, int>
    {
    }
}