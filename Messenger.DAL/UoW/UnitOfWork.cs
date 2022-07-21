using Messenger.DAL.Context;
using Messenger.DAL.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Messenger.DAL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        #region Properties

        public IUsersRepository Users { get; }

        public IUserImagesRepository UserImages { get; }

        public IChatsRepository Chats { get; }

        public IChatImagesRepository ChatImages { get; }

        public IMessagesRepository Messages { get; }

        public IUserAccountsRepository UserAccounts { get; }

        public IMessageImagesRepository MessageImages { get; }

        public IActionLogsRepository ActionLogs { get; }

        #endregion

        #region Constructor

        public UnitOfWork(AppDbContext context, 
            IUsersRepository usersRepository, 
            IUserImagesRepository userImagesRepository,
            IChatsRepository chatsRepository,
            IChatImagesRepository chatImagesRepository,
            IMessagesRepository messagesRepository,
            IMessageImagesRepository messageImagesRepository,
            IUserAccountsRepository userAccountsRepository,
            IActionLogsRepository actionLogsRepository)
        {
            _context = context;
            Users = usersRepository;
            UserImages = userImagesRepository;
            UserAccounts = userAccountsRepository;
            Chats = chatsRepository;
            ChatImages = chatImagesRepository;
            Messages = messagesRepository;
            MessageImages = messageImagesRepository;
            ActionLogs = actionLogsRepository;
        }

        #endregion

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
