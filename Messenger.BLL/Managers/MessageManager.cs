using AutoMapper;
using Messenger.BLL.Exceptions;
using Messenger.BLL.MessageImages;
using Messenger.BLL.Messages;
using Messenger.DAL.Entities;
using Messenger.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers
{
    public class MessageManager : IMessageManager
    {
        private readonly IMapper _mapper;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IMessageImagesRepository _messageImagesRepository;
        private readonly IUserAccountsRepository _userAccountsRepository;
        private const string PathToSave = "..\\Messenger.BLL\\Images\\";
        
        public MessageManager(IMapper mapper, 
                              IMessagesRepository messagesRepository, 
                              IMessageImagesRepository messageImagesRepository,
                              IUserAccountsRepository userAccountsRepository)
        {
            _mapper = mapper;
            _messagesRepository = messagesRepository;
            _messageImagesRepository = messageImagesRepository;
            _userAccountsRepository = userAccountsRepository;
        }

        public async Task<MessageViewModel> SendMessage (MessageCreateModel messageModel, string userId)
        {
            var userAccountEntity = _userAccountsRepository
                .GetAll()
                .Where(u => u.User.Id == userId && 
                u.User.Id == messageModel.UserId && !u.IsBanned && u.ChatId == messageModel.ChatId)
                .SingleOrDefault();

            if (userAccountEntity == null)
                throw new KeyNotFoundException();

            var messageEntity = _mapper.Map<Message>(messageModel);
            var messageViewModel = _mapper.Map<MessageViewModel>
                (await _messagesRepository.CreateAsync(messageEntity));

            var imageViewModelCollection = new List<MessageImageViewModel>();

            if (messageModel.Files != null)
            {
                foreach (var file in messageModel.Files)
                {
                    string filePath = PathToSave + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        await file.CopyToAsync(fileStream);
                    MessageImageCreateModel imageModel = new()
                    {
                        Path = filePath,
                        MessageId = messageViewModel.Id
                    };
                    var messageImageEntity = _mapper.Map<MessageImage>(imageModel);
                    imageViewModelCollection.Add(_mapper.Map<MessageImageViewModel>(messageImageEntity));
                    await _messageImagesRepository.CreateAsync(messageImageEntity);
                }
            }
            messageViewModel.Images = imageViewModelCollection;
            return messageViewModel;
        }

        public async Task<MessageViewModel> EditMessage(MessageUpdateModel messageModel, string userId)
        {
            var messageEntity = _messagesRepository.GetById(messageModel.Id);
            var userAccountEntity = _userAccountsRepository
                .GetAll()
                .Where(u => u.User.Id == userId &&
                u.User.Id == messageEntity.UserId && !u.IsBanned && u.ChatId == messageEntity.ChatId)
                .SingleOrDefault();

            if (userAccountEntity == null || messageEntity == null)
                throw new KeyNotFoundException();

            messageEntity.Text = messageModel.Text;
            var messageFile = messageModel.File; 

            if (messageFile != null)
            {
                var messageImageEntity = _messageImagesRepository.GetById(messageModel.ImageId);
                File.Delete(messageImageEntity.Path);
                string filePath = PathToSave + Guid.NewGuid().ToString() + Path.GetExtension(messageFile.FileName);
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                await messageFile.CopyToAsync(fileStream);
                messageImageEntity.Path = filePath;
                await _messageImagesRepository.UpdateAsync(messageImageEntity);
            }
            return _mapper.Map<MessageViewModel>(await _messagesRepository.UpdateAsync(messageEntity));
        }

        public async Task<bool> DeleteMessage(int messageId, string userId)
        {
            var messageEntity = await _messagesRepository.GetByIdAsync(messageId);

            var userAccountEntity = _userAccountsRepository
                .GetAll()
                .Where(u => u.User.Id == userId &&
                u.User.Id == messageEntity.UserId && !u.IsBanned && u.ChatId == messageEntity.ChatId)
                .SingleOrDefault();

            if (userAccountEntity == null || messageEntity == null)
                throw new KeyNotFoundException();

            return await _messagesRepository.DeleteByIdAsync(messageId);
        }

        public MessageViewModel GetMessage(int messageId)
        {
            //var messageEntity = _messagesRepository.GetById(messageId);
            var messageEntity = _messagesRepository.GetAll()
                .Where(u => u.Id == messageId)
                .SingleOrDefault();

            if (messageEntity == null)
                throw new KeyNotFoundException();

            return _mapper.Map<MessageViewModel>(messageEntity);
        }

        public IEnumerable<MessageViewModel> GetMessagesFromChat(int chatId, string userId, DateTime? date = null)
        {
            var userAccountEntity = _userAccountsRepository
                .GetAll() 
                .Where(u => u.User.Id == userId && u.Chat.Id == chatId)
                .SingleOrDefault();

            var messageEntityList = _messagesRepository
                .GetAll()
                .Where(predicate: u => u.ChatId == chatId && 
                (date == null || u.CreatedTime.Date == date.Value.Date))
                .ToList();

            if (userAccountEntity == null || messageEntityList == null)
                throw new KeyNotFoundException();

            var messageModelList = _mapper.Map<List<MessageViewModel>>(messageEntityList);
            return messageModelList;
        }
    }
}
