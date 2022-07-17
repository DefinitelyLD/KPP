using Messenger.DAL.Context;
using Messenger.DAL.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Messenger.DAL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUsersRepository Users { get; }
        public IChatsRepository Chats { get; }
        public IChatImagesRepository ChatImages { get; }
        public IMessagesRepository Messages { get; }
        public IUserAccountsRepository UserAccounts { get; }
        public IMessageImagesRepository MessageImages { get; }

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context, 
            IUsersRepository usersRepository, 
            IChatsRepository chatsRepository,
            IChatImagesRepository chatImagesRepository,
            IMessagesRepository messagesRepository,
            IMessageImagesRepository messageImagesRepository,
            IUserAccountsRepository userAccountsRepository)
        {
            _context = context;
            Users = usersRepository;
            UserAccounts = userAccountsRepository;
            Chats = chatsRepository;
            ChatImages = chatImagesRepository;
            Messages = messagesRepository;
            MessageImages = messageImagesRepository;
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
