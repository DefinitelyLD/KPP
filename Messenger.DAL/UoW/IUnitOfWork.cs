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
        IUsersRepository Users { get; }
        IChatsRepository Chats { get; }
        IMessagesRepository Messages { get; }
        int SaveChanges();
    }
}
