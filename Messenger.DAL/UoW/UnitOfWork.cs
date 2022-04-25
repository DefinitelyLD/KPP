using Messenger.DAL.Context;
using Messenger.DAL.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Messenger.DAL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUsersRepository UsersRepository { get; }
        public IChatsRepository ChatsRepository { get; }
        public IMessagesRepository MessagesRepository { get; }
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context, IUsersRepository usersRepository, IChatsRepository chatsRepository, IMessagesRepository messagesRepository)
        {
            _context = context;
            UsersRepository = usersRepository;
            ChatsRepository = chatsRepository;
            MessagesRepository = messagesRepository;
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
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
