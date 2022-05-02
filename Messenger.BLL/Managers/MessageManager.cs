using AutoMapper;
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
    public class MessageManager: IMessageManager
    {
        private readonly IMapper _mapper;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IMessageImagesRepository _messageImagesRepository;
        private const string FilePath = "..\\Messenger.BLL\\Images\\";
        
        public MessageManager(IMapper mapper, 
                              IMessagesRepository messagesRepository, 
                              IMessageImagesRepository messageImagesRepository)
        {
            _mapper = mapper;
            _messagesRepository = messagesRepository;
            _messageImagesRepository = messageImagesRepository;
        }

        public async Task<MessageViewModel> SendMessage (MessageCreateModel messageModel)
        {
            var messageEntity = _mapper.Map<Message>(messageModel);
            var messageViewModel = _mapper.Map<MessageViewModel>(_messagesRepository.Create(messageEntity));
            var imageViewModelCollection = new List<MessageImageViewModel>();

            if (messageModel.Files != null)
            {
                foreach (var file in messageModel.Files)
                {
                    string filePath = FilePath + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        await file.CopyToAsync(fileStream);
                    MessageImageCreateModel imageModel = new()
                    {
                        Path = filePath,
                        MessageId = messageViewModel.Id
                    };
                    var messageImageEntity = _mapper.Map<MessageImage>(imageModel);
                    imageViewModelCollection.Add(_mapper.Map<MessageImageViewModel>(messageImageEntity));
                    _messageImagesRepository.Create(messageImageEntity);
                }
            }

            messageViewModel.Images = imageViewModelCollection;

            return messageViewModel;
        }

        public async Task<MessageViewModel> EditMessage(MessageUpdateModel messageModel)
        {
            var messageEntity = _messagesRepository.GetById(messageModel.Id);
            messageEntity.Text = messageModel.Text;
            var messageFile = messageModel.File; 
            if (messageFile != null)
            {
                var messageImageEntity = _messageImagesRepository.GetById(messageModel.ImageId);
                File.Delete(messageImageEntity.Path);
                string filePath = FilePath + Guid.NewGuid().ToString() + Path.GetExtension(messageFile.FileName);
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                await messageFile.CopyToAsync(fileStream);
                messageImageEntity.Path = filePath;
                _messageImagesRepository.Update(messageImageEntity);
            }
            return _mapper.Map<MessageViewModel>(_messagesRepository.Update(messageEntity));
        }

        public bool DeleteMessage(int messageId)
        {
            return _messagesRepository.DeleteById(messageId);
        }

        public MessageViewModel GetMessage(int messageId)
        {
            Message messageEntity = _messagesRepository.GetById(messageId);
            return _mapper.Map<MessageViewModel>(messageEntity);
        }

        public IEnumerable<MessageViewModel> GetAllMessages()
        {
            var messageEntityList = _messagesRepository.GetAll().ToList();
            var messageModelList = _mapper.Map<List<MessageViewModel>>(messageEntityList);
            return messageModelList;
        }
    }
}
