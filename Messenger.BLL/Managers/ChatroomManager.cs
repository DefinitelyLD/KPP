using AutoMapper;
using Messenger.BLL.Chats;
using Messenger.BLL.Exceptions;
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

        public ChatViewModel CreateChatroom(ChatCreateModel chatModel, string userId)
        {
            if (chatModel.UserId != userId)
                throw new NotAllowedException("Incorrect user ID");

            var chatEntity = _mapper.Map<Chat>(chatModel);
            var chatViewModel = _mapper.Map<ChatViewModel>(_chatsRepository.Create(chatEntity));
            
            UserAccountCreateModel ownerAccountModel = new()
            {
                ChatId = chatViewModel.Id,
                UserId = chatModel.UserId,
                IsOwner = true,
                IsAdmin = true
            };
            var ownerAccountEntity = _mapper.Map<UserAccount>(ownerAccountModel);
            _userAccountsRepository.Create(ownerAccountEntity);

            return chatViewModel;
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
                throw new BadRequestException("The user is already in the chat."); 

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
                throw new BadRequestException("Owner can't leave the chat");

            return _userAccountsRepository.DeleteById(userAccountId);
        }

        public bool KickUser(UserAccountViewModel userAccountModel, string adminId)
        {
            var userAccountEntity = _userAccountsRepository.GetById(userAccountModel.Id);
            var adminAccountEntity = _userAccountsRepository.GetAll()
                                                            .Where(u => u.User.Id == adminId)
                                                            .SingleOrDefault();
            if (adminAccountEntity == null)
                throw new KeyNotFoundException();

            CheckModels(userAccountEntity, adminAccountEntity);

            if (userAccountEntity.IsAdmin && !adminAccountEntity.IsOwner)
                throw new BadRequestException("You can't kick the owner");

            return _userAccountsRepository.DeleteById(userAccountEntity.Id);
        }

        public UserAccountUpdateModel BanUser(UserAccountViewModel userAccountModel, string adminId)
        {
            var userAccountEntity = _userAccountsRepository.GetById(userAccountModel.Id);
            var adminAccountEntity = _userAccountsRepository.GetAll()
                                                .Where(u => u.User.Id == adminId)
                                                .SingleOrDefault();
            if (adminAccountEntity == null)
                throw new KeyNotFoundException();

            CheckModels(userAccountEntity, adminAccountEntity);

            if (userAccountEntity.IsOwner || userAccountEntity.IsBanned)
                throw new BadRequestException("You can't ban the owner or a banned user");

            userAccountEntity.IsBanned = true;
            return _mapper.Map<UserAccountUpdateModel>(_userAccountsRepository.Update(userAccountEntity));
        }

        public UserAccountUpdateModel UnbanUser(UserAccountViewModel userAccountModel, string adminId)
        {
            var userAccountEntity = _userAccountsRepository.GetById(userAccountModel.Id);
            var adminAccountEntity = _userAccountsRepository.GetAll()
                                                .Where(u => u.User.Id == adminId)
                                                .SingleOrDefault();
            if (adminAccountEntity == null)
                throw new KeyNotFoundException();

            CheckModels(userAccountEntity, adminAccountEntity);

            if (!userAccountEntity.IsBanned)
                throw new BadRequestException("You can't unban this user");

            userAccountEntity.IsBanned = false;
            return _mapper.Map<UserAccountUpdateModel>(_userAccountsRepository.Update(userAccountEntity));
        }

        public UserAccountUpdateModel SetAdmin(UserAccountViewModel userAccountModel, string adminId)
        {
            var userAccountEntity = _userAccountsRepository.GetById(userAccountModel.Id);
            var adminAccountEntity = _userAccountsRepository.GetAll()
                                                .Where(u => u.User.Id == adminId)
                                                .SingleOrDefault();
            if (adminAccountEntity == null)
                throw new KeyNotFoundException();

            CheckModels(userAccountEntity, adminAccountEntity);

            if (userAccountEntity.IsAdmin || userAccountEntity.IsBanned)
                throw new BadRequestException("This user is already admin or banned");

            userAccountEntity.IsAdmin = true;
            return _mapper.Map<UserAccountUpdateModel>(_userAccountsRepository.Update(userAccountEntity));
        }

        public UserAccountUpdateModel UnsetAdmin(UserAccountViewModel userAccountModel, string adminId)
        {
            var userAccountEntity = _userAccountsRepository.GetById(userAccountModel.Id);
            var adminAccountEntity = _userAccountsRepository.GetAll()
                                                .Where(u => u.User.Id == adminId)
                                                .SingleOrDefault();
            if (adminAccountEntity == null)
                throw new KeyNotFoundException();

            CheckModels(userAccountEntity, adminAccountEntity);

            if (!userAccountEntity.IsAdmin)
                throw new BadRequestException("This user is banned or not an admin");

            userAccountEntity.IsAdmin = false;
            return _mapper.Map<UserAccountUpdateModel>(_userAccountsRepository.Update(userAccountEntity));
        }

        public IEnumerable<UserAccountViewModel> GetAllBannedUsers(int chatId, string userName)
        {
            //throw KeyNotFoundException, if current user isn't in the chat
            ThrowExceptionIfUserIsNotInChat(chatId, userName);

            var bannedUsersEntityList = _userAccountsRepository
                .GetAll()
                .Where(u => u.IsBanned == true && u.ChatId == chatId)
                .ToList();

            var userModelList = _mapper.Map<List<UserAccountViewModel>>(bannedUsersEntityList);
            return userModelList;
        }

        public IEnumerable<UserAccountViewModel> GetAllAdmins(int chatId, string userName)
        {
            //throw KeyNotFoundException, if current user isn't in the chat
            ThrowExceptionIfUserIsNotInChat(chatId, userName); 

            var adminsEntityList = _userAccountsRepository
                .GetAll()
                .Where(u => u.IsAdmin == true && u.ChatId == chatId)
                .ToList();

            var userModelList = _mapper.Map<List<UserAccountViewModel>>(adminsEntityList);
            return userModelList;
        }

        public IEnumerable<UserAccountViewModel> GetAllUsers(int chatId, string userName)
        {
            //throw KeyNotFoundException, if current user isn't in the chat
            ThrowExceptionIfUserIsNotInChat(chatId, userName);

            var usersEntityList = _userAccountsRepository
                .GetAll()
                .Where(u => u.ChatId == chatId)
                .ToList();

            var userModelList = _mapper.Map<List<UserAccountViewModel>>(usersEntityList);
            return userModelList;
        }

        private static void CheckModels(UserAccount userAccountEntity, UserAccount adminAccountEntity)
        {
            if (userAccountEntity.ChatId != adminAccountEntity.ChatId)
                throw new BadRequestException("Users are in different chats");

            if (!adminAccountEntity.IsAdmin)
                throw new NotAllowedException("This action is for admins only");
        }

        private void ThrowExceptionIfUserIsNotInChat(int chatId, string userName)
        {
            var currentUserEntity = _userAccountsRepository
                .GetAll()
                .Where(u => u.User.UserName == userName && u.ChatId == chatId)
                .SingleOrDefault();

            if (currentUserEntity == null)
                throw new KeyNotFoundException();
        }

    }
}
