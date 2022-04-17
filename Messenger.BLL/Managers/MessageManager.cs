using AutoMapper;
using Messenger.BLL.CreateModels;
using Messenger.BLL.UpdateModels;
using Messenger.BLL.ViewModels;
using Messenger.DAL.Entities;
using Messenger.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers
{
    public class MessageManager: IMessageManager
    {
        private readonly IMapper _mapper;
        private readonly IMessagesRepository _messageRepository;
        
        public MessageManager(IMapper mapper, IMessagesRepository messageRepository)
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
        }

        public MessageCreateModel SendMessage (MessageCreateModel messageModel)
        {
            var msgEntity = _mapper.Map<Message>(messageModel);
            return _mapper.Map<MessageCreateModel>(_messageRepository.Create(msgEntity));
        }

        public MessageUpdateModel EditMessage(MessageUpdateModel messageModel)
        {
            var msgEntity = _mapper.Map<Message>(messageModel);
            return _mapper.Map<MessageUpdateModel>(_messageRepository.Update(msgEntity));
        }

        public bool DeleteMessage(int messageId)
        {
            return _messageRepository.DeleteById(messageId);
        }

        public MessageViewModel GetMessage(int messageId)
        {
            return _mapper.Map<MessageViewModel>(_messageRepository.GetById(messageId));
        }

        public IEnumerable<MessageViewModel> GetAllMessages()
        {
            var messageEntityList = _messageRepository.GetAll().ToList();
            var messageModelList = _mapper.Map<List<MessageViewModel>>(messageEntityList);
            return messageModelList;
        }
    }
}
