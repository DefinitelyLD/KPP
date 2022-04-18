using AutoMapper;
using Messenger.BLL.CreateModels;
using Messenger.BLL.UpdateModels;
using Messenger.BLL.ViewModels;
using Messenger.DAL.Entities;
using Messenger.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Messenger.BLL.Managers
{
    public class ChatroomManager: IChatroomManager
    {
        private readonly IMapper _mapper;
        private readonly IChatsRepository _chatsRepository;
        private readonly IUserAccountsRepository _userAccountsRepository;


        public ChatroomManager(IMapper mapper, 
                               IChatsRepository chatsRepository, 
                               IUserAccountsRepository userAccountsRepository)
        {
            _mapper = mapper;
            _chatsRepository = chatsRepository;
            _userAccountsRepository = userAccountsRepository;
        }

        public ChatUpdateModel EditChatroom(ChatUpdateModel chatModel)
        {
            var chatEntity = _mapper.Map<Chat>(chatModel);
            return _mapper.Map<ChatUpdateModel>(_chatsRepository.Update(chatEntity));
        }

        public bool DeleteChatroom(int chatId)
        {
            return _chatsRepository.DeleteById(chatId);
        }

        public ChatViewModel GetChatroom(int chatId)
        {
            return _mapper.Map<ChatViewModel>(_chatsRepository.GetById(chatId));
        }

        public IEnumerable<ChatViewModel> GetAllChatrooms()
        {
            var chatEntityList = _chatsRepository.GetAll().ToList();
            var chatModelList = _mapper.Map<List<ChatViewModel>>(chatEntityList);
            return chatModelList;
        }

        public UserAccountCreateModel AddToChatroom(int userId, int chatId)
        {
            UserAccountCreateModel userAccountModel = new()
            {
                ChatId = chatId,
                UserId = userId
            };
            var userAccountEntity = _mapper.Map<UserAccount>(userAccountModel);
            return _mapper.Map<UserAccountCreateModel>(_userAccountsRepository.Create(userAccountEntity));
        }

        public bool LeaveFromChatroom(int userAccountId)
        {
            return _userAccountsRepository.DeleteById(userAccountId);
        }

        public bool KickUser(int userAccountId)
        {
            return _userAccountsRepository.DeleteById(userAccountId);
        }

        public UserAccountUpdateModel BanUser(int userId, int chatId)
        {
            var userAccountEntity = _userAccountsRepository
                .GetAll()
                .Where(u => u.UserId == userId && u.ChatId == chatId)
                .SingleOrDefault();
            return _mapper.Map<UserAccountUpdateModel>(_userAccountsRepository.Update(userAccountEntity));
        }

        public UserAccountUpdateModel SetAdmin(int userId, int chatId)
        {
            var userAccountEntity = _userAccountsRepository
                .GetAll()
                .Where(u => u.UserId == userId && u.ChatId == chatId)
                .SingleOrDefault();
            return _mapper.Map<UserAccountUpdateModel>(_userAccountsRepository.Update(userAccountEntity));
        }


        public UserAccountUpdateModel UnsetAdmin(int userId, int chatId)
        {
            var userAccountEntity = _userAccountsRepository
                .GetAll()
                .Where(u => u.UserId == userId && u.ChatId == chatId)
                .SingleOrDefault();
            return _mapper.Map<UserAccountUpdateModel>(_userAccountsRepository.Update(userAccountEntity));
        }

        public IEnumerable<UserViewModel> GetAllAdmins(ChatViewModel chatModel)
        {
            var chatEntity = _mapper.Map<Chat>(chatModel);
            var adminsEntityList = chatEntity.Users.Where(u => u.IsAdmin == true).ToList();
            var userModelList = _mapper.Map<List<UserViewModel>>(adminsEntityList);
            return userModelList;
        }
    }
}
