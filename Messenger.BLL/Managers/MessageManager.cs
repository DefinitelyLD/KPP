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

        public MessageModel SendMessage (MessageModel msg)
        {
            var msgEntity = _mapper.Map<Message>(msg);
            return _mapper.Map<MessageModel>(_messageRep.Create(msgEntity));
        }

        public MessageModel EditMessage(MessageModel msg)
        {
            var msgEntity = _mapper.Map<Message>(msg);
            return _mapper.Map<MessageModel>(_messageRep.Update(msgEntity));
        }

        public bool DeleteMessage(MessageModel msg)
        {
            return _messageRep.DeleteById(msg.Id);
        }

        public MessageModel GetMessage(int msgId)
        {
            return _mapper.Map<MessageModel>(_messageRep.GetById(msgId));
        }

        public IEnumerable<MessageModel> GetAllMessages(MessageModel msg)
        {
            var msgEntityList = _messageRep.GetAll().ToList();
            var msgModelList =_mapper.Map<List<MessageModel>>(msgEntityList);
            return msgModelList;
        }
    }
}
