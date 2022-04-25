using Messenger.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
