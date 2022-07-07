using AutoMapper;
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
        private readonly IWebHostEnvironment _environment;
        private readonly string PathToSave;
        private readonly IUnitOfWork _unitOfWork;

        public MessageManager(IMapper mapper,
                              IWebHostEnvironment environment,
                              IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _environment = environment;
            PathToSave = _environment.WebRootPath + "\\Images\\";
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageViewModel> SendMessage (MessageCreateModel messageModel, string userId)
        {
            var userAccountEntity = _unitOfWork.UserAccounts
                .GetAll()
                .Where(u => u.User.Id == userId && 
                u.User.Id == messageModel.UserId && !u.IsBanned && u.ChatId == messageModel.ChatId)
                .SingleOrDefault();

            if (userAccountEntity == null)
                throw new KeyNotFoundException();

            var messageEntity = _mapper.Map<Message>(messageModel);
            var messageViewModel = _mapper.Map<MessageViewModel>
                (await _unitOfWork.Messages.CreateAsync(messageEntity));

            var imageViewModelCollection = new List<MessageImageViewModel>();

            if (messageModel.Files != null)
            {
                foreach (var file in messageModel.Files)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = PathToSave + fileName;
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        await file.CopyToAsync(fileStream);
                    MessageImageCreateModel imageModel = new()
                    {
                        Path = fileName,
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
                File.Delete(PathToSave + messageImageEntity.Path);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(messageFile.FileName);
                string filePath = PathToSave + fileName;
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                await messageFile.CopyToAsync(fileStream);
                messageImageEntity.Path = fileName;
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
            await _unitOfWork.Messages.UpdateAsync(messageEntity);

            return true;
        }

        public MessageViewModel GetMessage(int messageId)
        {
            //var messageEntity = _messagesRepository.GetById(messageId);
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
