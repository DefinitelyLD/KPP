using AutoMapper;
using Messenger.BLL.Models;
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
        private readonly IMessagesRepository _messageRep;
        
        public MessageManager(IMapper mapper, IMessagesRepository messageRep)
        {
            _mapper = mapper;
            _messageRep = messageRep;
        }

        public MessageModel ManagerSendMessage (MessageModel msg)
        {
            return _mapper.Map<MessageModel>(_messageRep.Create(_mapper.Map<Message>(msg)));
        }

        public MessageModel ManagerEditMessage(MessageModel msg)
        {
            return _mapper.Map<MessageModel>(_messageRep.Update(_mapper.Map<Message>(msg)));
        }

        public bool ManagerDeleteMessage(MessageModel msg)
        {
            return _messageRep.DeleteById(msg.Id);
        }

        public MessageModel ManagerGetMessage(MessageModel msg)
        {
            return _mapper.Map<MessageModel>(_messageRep.GetById(msg.Id));
        }
    }
}
