using AutoMapper;
using Messenger.BLL.Chats;
using Messenger.BLL.Exceptions;
using Messenger.BLL.UserAccounts;
using Messenger.DAL.Entities;
using Messenger.DAL.UoW;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers
{
    public class ChatroomManager : IChatroomManager
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ChatroomManager(IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ChatViewModel> CreateChatroom(ChatCreateModel chatModel, string userId)
        {
            if (chatModel.UserId != userId)
                throw new NotAllowedException("Incorrect user ID");

            var chatEntity = _mapper.Map<Chat>(chatModel);
            var chatViewModel = _mapper.Map<ChatViewModel>(await _unitOfWork.Chats.CreateAsync(chatEntity));
            
            UserAccountCreateModel ownerAccountModel = new()
            {
                ChatId = chatViewModel.Id,
                UserId = chatModel.UserId,
                IsOwner = true,
                IsAdmin = true
            };
            var ownerAccountEntity = _mapper.Map<UserAccount>(ownerAccountModel);
            await _unitOfWork.UserAccounts.CreateAsync(ownerAccountEntity);

            return chatViewModel;
        }

        public async Task<ChatUpdateModel> EditChatroom(ChatUpdateModel chatModel, string adminId)
        {
            var adminAccountEntity = _unitOfWork.UserAccounts.GetAll()
                .Where(u => u.User.Id == adminId &&
                u.Chat.Id == chatModel.Id && u.IsAdmin)
                .SingleOrDefault();

            if (adminAccountEntity == null)
                throw new KeyNotFoundException();

            var chatEntity = _mapper.Map<Chat>(chatModel);

            return _mapper.Map<ChatUpdateModel>(await _unitOfWork.Chats.UpdateAsync(chatEntity));
        }

        public async Task<bool> DeleteChatroom(int chatId, string userId)
        {
            var userAccountEntity = _unitOfWork.UserAccounts.GetAll()
                .Where(u => u.User.Id == userId && u.Chat.Id == chatId && u.IsOwner)
                .SingleOrDefault();

            if (userAccountEntity == null)
                throw new KeyNotFoundException();

            var chatEntity = await _unitOfWork.Chats.GetByIdAsync(chatId);
            chatEntity.IsDeleted = true;
            var result = await _unitOfWork.Chats.UpdateAsync(chatEntity);

            return result.IsDeleted;
        }

        public ChatViewModel GetChatroom(int chatId, string userId)
        {
            var userAccountEntity = _unitOfWork.UserAccounts.GetAll()
                .Where(u => u.User.Id == userId && u.Chat.Id == chatId && !u.IsLeft)
                .SingleOrDefault();

            var chatEntity = _unitOfWork.Chats.GetAll()
                .Where(u => u.Id == chatId && u.Users.Contains(userAccountEntity) && !u.IsDeleted)
                .SingleOrDefault();

            if (userAccountEntity == null || chatEntity == null)
                throw new KeyNotFoundException();

            return _mapper.Map<ChatViewModel>(chatEntity);
        }

        public IEnumerable<ChatViewModel> GetAllChatrooms(string userId)
        {
            var userAccountEntityList = _unitOfWork.UserAccounts.GetAll()
                .Where(u => u.User.Id == userId && !u.IsLeft)
                .ToList();

            var chatIdList = new List<int>();
            foreach (var userAccountEntity in userAccountEntityList)
            {
                chatIdList.Add(userAccountEntity.ChatId);
            }

            var chatEntityList = _unitOfWork.Chats
                .GetAll()
                .Where(u => chatIdList.Contains(u.Id) && !u.IsDeleted)
                .ToList();

            var chatModelList = _mapper.Map<List<ChatViewModel>>(chatEntityList);

            return chatModelList;
        }

        public async Task<UserAccountCreateModel> AddToChatroom(string userId, int chatId, string currentUserId)
        {
            var currentUserEntity = _unitOfWork.Users.GetById(currentUserId);

            if (currentUserEntity.BlockedUsersFrom.Where(x => x.Id == userId).SingleOrDefault() != null
                || currentUserEntity.BlockedUsersTo.Where(x => x.Id == userId).SingleOrDefault() != null)
                throw new BadRequestException("You cannot add this user to the chat.");

            var userAccountExistingEntity = _unitOfWork.UserAccounts.GetAll()
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

            return _mapper.Map<UserAccountCreateModel>(await _unitOfWork.UserAccounts.CreateAsync(userAccountNewEntity));
        }

        public async Task<bool> LeaveFromChatroom(int chatId, string userId)
        { 
            var userAccountEntity = _unitOfWork.UserAccounts.GetAll()
                .Where(p => p.UserId == userId && p.ChatId == chatId)
                .SingleOrDefault();

            if (userAccountEntity.IsOwner)
                throw new BadRequestException("Owner can't leave the chat");

            userAccountEntity.IsLeft = true;
            userAccountEntity.IsAdmin = false;
            var result = await _unitOfWork.UserAccounts.UpdateAsync(userAccountEntity);

            return result.IsLeft;
        }

        public async Task<bool> KickUser(int userAccountId, string adminId)
        {
            var userAccountEntity = _unitOfWork.UserAccounts.GetAll()
                .Where(u => u.Id == userAccountId)
                .SingleOrDefault();

            var adminAccountEntity = _unitOfWork.UserAccounts.GetAll()
                .Where(u => u.User.Id == adminId &&
                u.Chat.Id == userAccountEntity.Chat.Id && u.IsAdmin)
                .SingleOrDefault();

            if (adminAccountEntity == null || userAccountEntity == null)
                throw new KeyNotFoundException();

            if (userAccountEntity.IsAdmin && !adminAccountEntity.IsOwner)
                throw new BadRequestException("You can't kick the admin");

            userAccountEntity.IsLeft = true;
            userAccountEntity.IsAdmin = false;
            var result = await _unitOfWork.UserAccounts.UpdateAsync(userAccountEntity);

            return result.IsLeft;
        }

        public async Task<UserAccountUpdateModel> BanUser(int userAccountId, string adminId)
        {
            var userAccountEntity = _unitOfWork.UserAccounts.GetAll()
                .Where(u => u.Id == userAccountId)
                .SingleOrDefault();

            var adminAccountEntity = _unitOfWork.UserAccounts.GetAll()
                .Where(u => u.User.Id == adminId &&
                u.Chat.Id == userAccountEntity.Chat.Id && u.IsAdmin)
                .SingleOrDefault();

            if (adminAccountEntity == null || userAccountEntity == null)
                throw new KeyNotFoundException();

            userAccountEntity.IsBanned = true;
            userAccountEntity.IsAdmin = false;

            return _mapper.Map<UserAccountUpdateModel>(await _unitOfWork.UserAccounts.UpdateAsync(userAccountEntity));
        }

        public async Task<UserAccountUpdateModel> UnbanUser(int userAccountId, string adminId)
        {
            var userAccountEntity = _unitOfWork.UserAccounts.GetAll()
                .Where(u => u.Id == userAccountId)
                .SingleOrDefault();

            var adminAccountEntity = _unitOfWork.UserAccounts.GetAll()
                .Where(u => u.User.Id == adminId &&
                u.Chat.Id == userAccountEntity.Chat.Id && u.IsAdmin)
                .SingleOrDefault();

            if (adminAccountEntity == null || userAccountEntity == null)
                throw new KeyNotFoundException();

            userAccountEntity.IsBanned = false;

            return _mapper.Map<UserAccountUpdateModel>(await _unitOfWork.UserAccounts.UpdateAsync(userAccountEntity));
        }

        public async Task<UserAccountUpdateModel> SetAdmin(int userAccountId, string adminId)
        {
            var userAccountEntity = _unitOfWork.UserAccounts.GetAll()
                .Where(u => u.Id == userAccountId)
                .SingleOrDefault();

            var adminAccountEntity = _unitOfWork.UserAccounts.GetAll()
                .Where(u => u.User.Id == adminId &&
                u.Chat.Id == userAccountEntity.Chat.Id && u.IsAdmin)
                .SingleOrDefault();

            if (adminAccountEntity == null || userAccountEntity == null)
                throw new KeyNotFoundException();

            userAccountEntity.IsAdmin = true;

            return _mapper.Map<UserAccountUpdateModel>(await _unitOfWork.UserAccounts.UpdateAsync(userAccountEntity));
        }

        public async Task<UserAccountUpdateModel> UnsetAdmin(int userAccountId, string adminId)
        {
            var userAccountEntity = _unitOfWork.UserAccounts.GetAll()
                .Where(u => u.Id == userAccountId && !u.IsOwner)
                .SingleOrDefault();

            var adminAccountEntity = _unitOfWork.UserAccounts.GetAll()
                .Where(u => u.User.Id == adminId && 
                u.Chat.Id == userAccountEntity.Chat.Id && u.IsAdmin)
                .SingleOrDefault();

            if (adminAccountEntity == null || userAccountEntity == null)
                throw new KeyNotFoundException();

            userAccountEntity.IsAdmin = false;

            return _mapper.Map<UserAccountUpdateModel>(await _unitOfWork.UserAccounts.UpdateAsync(userAccountEntity));
        }

        public UserAccountViewModel GetOwner(int chatId, string userId)
        {
            //throw KeyNotFoundException, if current user isn't in the chat
            ThrowExceptionIfUserIsNotInChat(chatId, userId);

            var ownerEntity = _unitOfWork.UserAccounts.GetAll()
                .Where(u => u.IsOwner && u.ChatId == chatId)
                .SingleOrDefault();

            var userModel = _mapper.Map<UserAccountViewModel>(ownerEntity);

            return userModel;
        }

        public IEnumerable<UserAccountViewModel> GetAllBannedUsers(int chatId, string userId)
        {
            //throw KeyNotFoundException, if current user isn't in the chat
            ThrowExceptionIfUserIsNotInChat(chatId, userId);

            var bannedUsersEntityList = _unitOfWork.UserAccounts
                .GetAll()
                .Where(u => u.IsBanned && u.ChatId == chatId)
                .ToList();

            var userModelList = _mapper.Map<List<UserAccountViewModel>>(bannedUsersEntityList);

            return userModelList;
        }

        public IEnumerable<UserAccountViewModel> GetAllAdmins(int chatId, string userId)
        {
            //throw KeyNotFoundException, if current user isn't in the chat
            ThrowExceptionIfUserIsNotInChat(chatId, userId); 

            var adminsEntityList = _unitOfWork.UserAccounts
                .GetAll()
                .Where(u => u.IsAdmin && u.ChatId == chatId)
                .ToList();

            var userModelList = _mapper.Map<List<UserAccountViewModel>>(adminsEntityList);

            return userModelList;
        }

        public IEnumerable<UserAccountViewModel> GetAllUsers(int chatId, string userId)
        {
            //throw KeyNotFoundException, if current user isn't in the chat
            ThrowExceptionIfUserIsNotInChat(chatId, userId);

            var usersEntityList = _unitOfWork.UserAccounts
                .GetAll()
                .Where(u => !u.IsLeft && u.ChatId == chatId)
                .ToList();

            var userModelList = _mapper.Map<List<UserAccountViewModel>>(usersEntityList);

            return userModelList;
        }

        private void ThrowExceptionIfUserIsNotInChat(int chatId, string userId)
        {
            var currentUserEntity = _unitOfWork.UserAccounts
                .GetAll()
                .Where(u => u.User.Id == userId && u.ChatId == chatId)
                .SingleOrDefault();

            if (currentUserEntity == null)
                throw new KeyNotFoundException();
        }
    }
}
