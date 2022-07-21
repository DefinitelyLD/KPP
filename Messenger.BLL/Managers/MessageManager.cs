using AutoMapper;
using Messenger.BLL.Exceptions;
using Messenger.BLL.MessageImages;
using Messenger.BLL.Messages;
using Messenger.DAL.Entities;
using Messenger.DAL.UoW;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers
{
    public class MessageManager : IMessageManager
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageManager _imageManager;

        public MessageManager(IMapper mapper,
                              IUnitOfWork unitOfWork,
                              IImageManager imageManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _imageManager = imageManager;
        }

        public async Task<MessageViewModel> SendMessage (MessageCreateModel messageModel, string userId)
        {
            var userAccountEntity = _unitOfWork.UserAccounts
                .GetAll()
                .Where(u => u.User.Id == userId && !u.IsBanned && u.ChatId == messageModel.ChatId)
                .SingleOrDefault();

            if (userAccountEntity == null)
            {
                throw new KeyNotFoundException();
            }

            var messageEntity = _mapper.Map<Message>(messageModel);
            var chatEntity = userAccountEntity.Chat;

            if (chatEntity.IsAdminsRoom == true)
            {
                throw new NotAllowedException("You are not allowed to send messages in this room.");
            }

            messageEntity.UserId = userId;
            var messageViewModel = _mapper.Map<MessageViewModel>
                (await _unitOfWork.Messages.CreateAsync(messageEntity));

            var imageViewModelCollection = new List<MessageImageViewModel>();

            if (messageModel.Files != null)
            {
                foreach (var file in messageModel.Files)
                {
                    var filePath = await _imageManager.UploadImage(file);

                    MessageImageCreateModel imageModel = new()
                    {
                        Path = filePath,
                        MessageId = messageViewModel.Id
                    };
                    var messageImageEntity = _mapper.Map<MessageImage>(imageModel);
                    imageViewModelCollection.Add(_mapper.Map<MessageImageViewModel>(messageImageEntity));
                    await _unitOfWork.MessageImages.CreateAsync(messageImageEntity);
                }
            }
            messageViewModel.Images = imageViewModelCollection;

            return messageViewModel;
        }

        public async Task<MessageViewModel> SendAdminsMessage(MessageCreateModel messageModel, string userId)
        {
            var userAccountEntity = _unitOfWork.UserAccounts
                .GetAll()
                .Where(u => u.User.Id == userId && !u.IsBanned && u.ChatId == messageModel.ChatId)
                .SingleOrDefault();

            if (userAccountEntity == null)
            {
                throw new KeyNotFoundException();
            }

            var messageEntity = _mapper.Map<Message>(messageModel);
            messageEntity.UserId = userId;
            var messageViewModel = _mapper.Map<MessageViewModel>
                (await _unitOfWork.Messages.CreateAsync(messageEntity));

            var imageViewModelCollection = new List<MessageImageViewModel>();

            if (messageModel.Files != null)
            {
                foreach (var file in messageModel.Files)
                {
                    var filePath = await _imageManager.UploadImage(file);

                    MessageImageCreateModel imageModel = new()
                    {
                        Path = filePath,
                        MessageId = messageViewModel.Id
                    };
                    var messageImageEntity = _mapper.Map<MessageImage>(imageModel);
                    imageViewModelCollection.Add(_mapper.Map<MessageImageViewModel>(messageImageEntity));
                    await _unitOfWork.MessageImages.CreateAsync(messageImageEntity);
                }
            }
            messageViewModel.Images = imageViewModelCollection;

            return messageViewModel;
        }

        public async Task<MessageViewModel> EditMessage(MessageUpdateModel messageModel, string userId)
        {
            var messageEntity = _unitOfWork.Messages.GetById(messageModel.Id);
            var userAccountEntity = _unitOfWork.UserAccounts
                .GetAll()
                .Where(u => u.User.Id == userId &&
                u.User.Id == messageEntity.UserId && !u.IsBanned && u.ChatId == messageEntity.ChatId)
                .SingleOrDefault();

            if (userAccountEntity == null)
                throw new KeyNotFoundException();

            messageEntity.Text = messageModel.Text;
            var messageFile = messageModel.File; 

            if (messageFile != null)
            {
                var messageImageEntity = _unitOfWork.MessageImages.GetById(messageModel.ImageId);
                File.Delete(messageImageEntity.Path);
                var filePath = await _imageManager.UploadImage(messageFile);
                messageImageEntity.Path = filePath;
                await _unitOfWork.MessageImages.UpdateAsync(messageImageEntity);
            }

            return _mapper.Map<MessageViewModel>(await _unitOfWork.Messages.UpdateAsync(messageEntity));
        }

        public async Task<bool> DeleteMessage(int messageId, string userId)
        {
            var messageEntity = await _unitOfWork.Messages.GetByIdAsync(messageId);

            var userAccountEntity = _unitOfWork.UserAccounts
                .GetAll()
                .Where(u => u.User.Id == userId &&
                u.User.Id == messageEntity.UserId && !u.IsBanned && !u.IsLeft && u.ChatId == messageEntity.ChatId)
                .SingleOrDefault();

            if (userAccountEntity == null)
                throw new KeyNotFoundException();

            messageEntity.IsDeleted = true;
            var result = await _unitOfWork.Messages.UpdateAsync(messageEntity);

            return result.IsDeleted;
        }

        public MessageViewModel GetMessage(int messageId)
        {
            var messageEntity = _unitOfWork.Messages.GetAll()
                .Where(u => u.Id == messageId && !u.IsDeleted)
                .SingleOrDefault();

            if (messageEntity == null)
                throw new KeyNotFoundException();

            return _mapper.Map<MessageViewModel>(messageEntity);
        }

        public IEnumerable<MessageViewModel> GetMessagesFromChat(int chatId, string userId, DateTime? date = null)
        {
            var userAccountEntity = _unitOfWork.UserAccounts
                .GetAll() 
                .Where(u => u.User.Id == userId && u.Chat.Id == chatId)
                .SingleOrDefault();

            var messageEntityList = _unitOfWork.Messages
                .GetAll()
                .Where(predicate: u => u.ChatId == chatId && !u.IsDeleted && 
                (date == null || u.CreatedTime.Date == date.Value.Date))
                .ToList();

            if (userAccountEntity == null || messageEntityList == null)
                throw new KeyNotFoundException();

            var messageModelList = _mapper.Map<List<MessageViewModel>>(messageEntityList);

            return messageModelList;
        }
    }
}
