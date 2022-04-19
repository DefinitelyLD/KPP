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
            if (chatEntity == null)
            {
                throw new KeyNotFoundException();
            }
            else
            {
                return _mapper.Map<ChatViewModel>(chatEntity);
            }
        }

        public IEnumerable<ChatViewModel> GetAllChatrooms()
        {
            var chatEntityList = _chatsRepository.GetAll().ToList();
            var chatModelList = _mapper.Map<List<ChatViewModel>>(chatEntityList);
            return chatModelList;
        }

        public UserAccountCreateModel AddToChatroom(int userId, int chatId)
        {
            var userAccountExistingEntity = _userAccountsRepository.GetAll()
                .Where(p => p.UserId == userId && p.ChatId == chatId)
                .FirstOrDefault();
            if (userAccountExistingEntity == null)
            {
                UserAccountCreateModel userAccountModel = new()
                {
                    ChatId = chatId,
                    UserId = userId
                };
                var userAccountNewEntity = _mapper.Map<UserAccount>(userAccountModel);
                return _mapper.Map<UserAccountCreateModel>(_userAccountsRepository.Create(userAccountNewEntity));
            } 
            else if(userAccountExistingEntity != null && userAccountExistingEntity.IsBanned == true)
            {
                throw new Exception("The user is already banned.");
            }
            else
            {
                throw new Exception("The user is already in the chat."); //Temporary solution
            }
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
            if (userAccountEntity == null)
            {
                throw new KeyNotFoundException();
            }
            else
            {
                userAccountEntity.IsBanned = true;
                return _mapper.Map<UserAccountUpdateModel>(_userAccountsRepository.Update(userAccountEntity));
            }
        }

        public UserAccountUpdateModel UnbanUser(int userId, int chatId)
        {
            var userAccountEntity = _userAccountsRepository
                .GetAll()
                .Where(u => u.UserId == userId && u.ChatId == chatId)
                .SingleOrDefault();
            if (userAccountEntity == null)
            {
                throw new KeyNotFoundException();
            }
            else
            {
                userAccountEntity.IsBanned = false;
                return _mapper.Map<UserAccountUpdateModel>(_userAccountsRepository.Update(userAccountEntity));
            }
        }

        public UserAccountUpdateModel SetAdmin(int userId, int chatId)
        {
            var userAccountEntity = _userAccountsRepository
                .GetAll()
                .Where(u => u.UserId == userId && u.ChatId == chatId)
                .SingleOrDefault();
            if (userAccountEntity == null)
            {
                throw new KeyNotFoundException();
            }
            else
            {
                userAccountEntity.IsAdmin = true;
                return _mapper.Map<UserAccountUpdateModel>(_userAccountsRepository.Update(userAccountEntity));
            }
        }


        public UserAccountUpdateModel UnsetAdmin(int userId, int chatId)
        {
            var userAccountEntity = _userAccountsRepository
                .GetAll()
                .Where(u => u.UserId == userId && u.ChatId == chatId)
                .SingleOrDefault();
            if (userAccountEntity == null)
            {
                throw new KeyNotFoundException();
            }
            else
            {
                userAccountEntity.IsAdmin = false;
                return _mapper.Map<UserAccountUpdateModel>(_userAccountsRepository.Update(userAccountEntity));
            }
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
