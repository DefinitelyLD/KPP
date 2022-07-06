using Messenger.DAL.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Messenger.DAL.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IUsersRepository UsersRepository { get; }
        IChatsRepository ChatsRepository { get; }
        IMessagesRepository MessagesRepository { get; }
        public int Save();
        public Task<int> SaveAsync();
    }
}
