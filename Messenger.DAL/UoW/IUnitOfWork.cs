using Messenger.DAL.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Messenger.DAL.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IUsersRepository Users { get; }

        IUserImagesRepository UserImages { get; }

        IUserAccountsRepository UserAccounts { get; }

        IChatsRepository Chats { get; }

        IChatImagesRepository ChatImages { get; }

        IMessagesRepository Messages { get; }

        IMessageImagesRepository MessageImages { get; }

        IActionLogsRepository ActionLogs { get; }

        public int Save();

        public Task<int> SaveAsync();
    }
}
