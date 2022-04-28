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
        private const string PathToSave = "..\\Messenger.BLL\\Images\\";
        
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

            if (messageModel.Files == null)
                return messageViewModel;

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
                _messageImagesRepository.Create(messageImageEntity);
            }

            return messageViewModel;
        }

        public MessageViewModel EditMessage(MessageUpdateModel messageModel)
        {
            var msgEntity = _mapper.Map<Message>(messageModel);
            return _mapper.Map<MessageViewModel>(_messagesRepository.Update(msgEntity));
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
            var messageModelList = _mapper.Map<IEnumerable<MessageViewModel>>(messageEntityList);
            return messageModelList;
        }
    }
}
