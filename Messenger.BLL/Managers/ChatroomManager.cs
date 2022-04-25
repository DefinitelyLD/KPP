using AutoMapper;
using Messenger.BLL.Chats;
using Messenger.BLL.UserAccounts;
using Messenger.BLL.Users;
using Messenger.DAL.Entities;
using Messenger.DAL.Repositories.Interfaces;
using System;
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
            Chat chatEntity = _chatsRepository.GetById(chatId);

            return _mapper.Map<ChatViewModel>(chatEntity);
        }

        public IEnumerable<ChatViewModel> GetAllChatrooms()
        {
            var chatEntityList = _chatsRepository.GetAll().ToList();
            var chatModelList = _mapper.Map<List<ChatViewModel>>(chatEntityList);
            return chatModelList;
        }

        public UserAccountCreateModel AddToChatroom(string userId, int chatId)
        {
            var userAccountExistingEntity = _userAccountsRepository.GetAll()
                .Where(p => p.UserId == userId && p.ChatId == chatId)
                .SingleOrDefault();

            if (userAccountExistingEntity != null)
                //TODO: should be done in #12 issue
                throw new Exception("The user is already in the chat."); 

            UserAccountCreateModel userAccountModel = new()
            {
                ChatId = chatId,
                UserId = userId
            };
            var userAccountNewEntity = _mapper.Map<UserAccount>(userAccountModel);
            return _mapper.Map<UserAccountCreateModel>(_userAccountsRepository.Create(userAccountNewEntity));
        }

        public bool LeaveFromChatroom(int userAccountId)
        {
            var userAccountEntity = _userAccountsRepository.GetById(userAccountId);
            if (userAccountEntity.IsOwner)
                throw new Exception("Owner can't leave the chat");

            return _userAccountsRepository.DeleteById(userAccountId);
        }

        public bool KickUser(UserAccountViewModel userAccountModel,
                             UserAccountViewModel adminAccountModel)
        {
            var userAccountEntity = _userAccountsRepository.GetById(userAccountModel.Id);
            var adminAccountEntity = _userAccountsRepository.GetById(adminAccountModel.Id);

            CheckModels(userAccountEntity, adminAccountEntity);

            if (userAccountEntity.IsAdmin && !adminAccountEntity.IsOwner)
                throw new Exception("You can't kick the owner");

            return _userAccountsRepository.DeleteById(userAccountEntity.Id);
        }

        public UserAccountUpdateModel BanUser(UserAccountViewModel userAccountModel, 
                                              UserAccountViewModel adminAccountModel)
        {
            var userAccountEntity = _userAccountsRepository.GetById(userAccountModel.Id);
            var adminAccountEntity = _userAccountsRepository.GetById(adminAccountModel.Id);

            CheckModels(userAccountEntity, adminAccountEntity);

            if (userAccountEntity.IsOwner || userAccountEntity.IsBanned)
                throw new Exception("You can't ban the owner or a banned user");

            userAccountEntity.IsBanned = true;
            return _mapper.Map<UserAccountUpdateModel>(_userAccountsRepository.Update(userAccountEntity));
        }

        public UserAccountUpdateModel UnbanUser(UserAccountViewModel userAccountModel,
                                                UserAccountViewModel adminAccountModel)
        {
            var userAccountEntity = _userAccountsRepository.GetById(userAccountModel.Id);
            var adminAccountEntity = _userAccountsRepository.GetById(adminAccountModel.Id);

            CheckModels(userAccountEntity, adminAccountEntity);

            if (!userAccountEntity.IsBanned)
                throw new Exception("You can't unban this user");

            userAccountEntity.IsBanned = false;
            return _mapper.Map<UserAccountUpdateModel>(_userAccountsRepository.Update(userAccountEntity));
            
        }

        public IEnumerable<UserViewModel> GetAllBannedUsers(ChatViewModel chatModel)
        {
            var chatEntity = _mapper.Map<Chat>(chatModel);
            var bannedUsersEntityList = chatEntity.Users.Where(u => u.IsBanned == true).ToList();
            var userModelList = _mapper.Map<List<UserViewModel>>(bannedUsersEntityList);
            return userModelList;
        }

        public UserAccountUpdateModel SetAdmin(UserAccountViewModel userAccountModel,
                                               UserAccountViewModel adminAccountModel)
        {
            var userAccountEntity = _userAccountsRepository.GetById(userAccountModel.Id);
            var adminAccountEntity = _userAccountsRepository.GetById(adminAccountModel.Id);

            CheckModels(userAccountEntity, adminAccountEntity);

            if (userAccountEntity.IsAdmin || userAccountEntity.IsBanned)
                throw new Exception("This user is already admin or banned");

            userAccountEntity.IsAdmin = true;
            return _mapper.Map<UserAccountUpdateModel>(_userAccountsRepository.Update(userAccountEntity));
        }

        public UserAccountUpdateModel UnsetAdmin(UserAccountViewModel userAccountModel,
                                                 UserAccountViewModel adminAccountModel)
        {
            var userAccountEntity = _userAccountsRepository.GetById(userAccountModel.Id);
            var adminAccountEntity = _userAccountsRepository.GetById(adminAccountModel.Id);

            CheckModels(userAccountEntity, adminAccountEntity);

            if (!userAccountEntity.IsAdmin)
                throw new Exception("This user is banned or not an admin");

            userAccountEntity.IsAdmin = false;
            return _mapper.Map<UserAccountUpdateModel>(_userAccountsRepository.Update(userAccountEntity));
        }

        public IEnumerable<UserViewModel> GetAllAdmins(ChatViewModel chatModel)
        {
            var chatEntity = _mapper.Map<Chat>(chatModel);
            var adminsEntityList = chatEntity.Users.Where(u => u.IsAdmin == true).ToList();
            var userModelList = _mapper.Map<List<UserViewModel>>(adminsEntityList);
            return userModelList;
        }

        private static void CheckModels(UserAccount userAccountEntity, UserAccount adminAccountEntity)
        {
            if (userAccountEntity.ChatId != adminAccountEntity.ChatId)
                throw new Exception("Users are in different chats");

            if (!adminAccountEntity.IsAdmin)
                throw new Exception("This action is for admins only");
        }
    }
}
