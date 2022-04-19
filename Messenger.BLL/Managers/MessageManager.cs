using AutoMapper;
using Messenger.BLL.Messages;
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
        private readonly IMessagesRepository _messagesRepository;
        
        public MessageManager(IMapper mapper, IMessagesRepository messagesRepository)
        {
            _mapper = mapper;
            _messagesRepository = messagesRepository;
        }

        public MessageCreateModel SendMessage (MessageCreateModel messageModel)
        {
            var msgEntity = _mapper.Map<Message>(messageModel);
            return _mapper.Map<MessageCreateModel>(_messagesRepository.Create(msgEntity));
        }

        public MessageUpdateModel EditMessage(MessageUpdateModel messageModel)
        {
            var msgEntity = _mapper.Map<Message>(messageModel);
            return _mapper.Map<MessageUpdateModel>(_messagesRepository.Update(msgEntity));
        }

        public bool DeleteMessage(int messageId)
        {
            return _messagesRepository.DeleteById(messageId);
        }

        public MessageViewModel GetMessage(int messageId)
        {
            Message messageEntity = _messagesRepository.GetById(messageId);
            if (messageEntity == null) throw new KeyNotFoundException();
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
