using Messenger.DAL.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Messenger.DAL.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IUsersRepository Users { get; }
        IUserAccountsRepository UserAccounts { get; }
        IChatsRepository Chats { get; }
        IMessagesRepository Messages { get; }
        IMessageImagesRepository MessageImages { get; }
        public int Save();
        public Task<int> SaveAsync();
    }
}
