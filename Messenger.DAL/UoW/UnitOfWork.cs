using Messenger.DAL.Context;
using Messenger.DAL.Repositories.Interfaces;
using System;

namespace Messenger.DAL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUsersRepository Users { get; }
        public IChatsRepository Chats { get; }
        public IMessagesRepository Messages { get; }
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context, IUsersRepository usersRepository, IChatsRepository chatsRepository, IMessagesRepository messagesRepository)
        {
            _context = context;
            Users = usersRepository;
            Chats = chatsRepository;
            Messages = messagesRepository;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
