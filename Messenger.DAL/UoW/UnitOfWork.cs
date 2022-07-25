using Messenger.DAL.Context;
using Messenger.DAL.Repositories;
using Messenger.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Messenger.DAL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        #region Fields

        private IActionLogsRepository _actionLogsRepository;
        private IChatImagesRepository _chatImagesRepository;
        private IChatsRepository _chatsRepository;
        private IMessageImagesRepository _messageImagesRepository;
        private IMessagesRepository _messagesRepository;
        private IUserAccountsRepository _userAccountsRepository;
        private IUserImagesRepository _userImagesRepository;
        private IUsersRepository _usersRepository;

        #endregion

        #region Constructor

        public UnitOfWork(DbContextOptions<AppDbContext> contextOptions)
        {
            _context = new AppDbContext(contextOptions);
        }

        #endregion

        #region Properties

        public IActionLogsRepository ActionLogs
        {
            get
            {
                if (_actionLogsRepository == null)
                    _actionLogsRepository = new ActionLogsRepository(_context);

                return _actionLogsRepository;
            }
        }

        public IChatImagesRepository ChatImages
        {
            get
            {
                if (_chatImagesRepository == null)
                    _chatImagesRepository = new ChatImagesRepository(_context);

                return _chatImagesRepository;
            }
        }

        public IMessageImagesRepository MessageImages
        {
            get
            {
                if (_messageImagesRepository == null)
                    _messageImagesRepository = new MessageImagesRepository(_context);

                return _messageImagesRepository;
            }
        }

        public IMessagesRepository Messages
        {
            get
            {
                if (_messagesRepository == null)
                    _messagesRepository = new MessagesRepository(_context);

                return _messagesRepository;
            }
        }

        public IUsersRepository Users
        {
            get
            {
                if (_usersRepository == null)
                    _usersRepository = new UsersRepository(_context);

                return _usersRepository;
            }
        }

        public IUserAccountsRepository UserAccounts
        {
            get
            {
                if (_userAccountsRepository == null)
                    _userAccountsRepository = new UserAccountsRepository(_context);

                return _userAccountsRepository;
            }
        }

        public IUserImagesRepository UserImages
        {
            get
            {
                if (_userImagesRepository == null)
                    _userImagesRepository = new UserImagesRepository(_context);

                return _userImagesRepository;
            }
        }

        public IChatsRepository Chats
        {
            get
            {
                if (_chatsRepository == null)
                    _chatsRepository = new ChatsRepository(_context);

                return _chatsRepository;
            }
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
