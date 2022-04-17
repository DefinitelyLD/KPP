using AutoMapper;
using Messenger.BLL.Models;
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
        private readonly IMessagesRepository _messageRep;
        
        public MessageManager(IMapper mapper, IMessagesRepository messageRep)
        {
            _mapper = mapper;
            _messageRep = messageRep;
        }

        public MessageCreateModel SendMessage (MessageCreateModel msg)
        {
            var msgEntity = _mapper.Map<Message>(msg);
            return _mapper.Map<MessageCreateModel>(_messageRep.Create(msgEntity));
        }

        public MessageUpdateModel EditMessage(MessageUpdateModel msg)
        {
            var msgEntity = _mapper.Map<Message>(msg);
            return _mapper.Map<MessageUpdateModel>(_messageRep.Update(msgEntity));
        }

        public bool DeleteMessage(int msgId)
        {
            return _messageRep.DeleteById(msgId);
        }

        public MessageViewModel GetMessage(int msgId)
        {
            return _mapper.Map<MessageViewModel>(_messageRep.GetById(msgId));
        }

        public IEnumerable<MessageViewModel> GetAllMessages()
        {
            var msgEntityList = _messageRep.GetAll().ToList();
            var msgModelList =_mapper.Map<List<MessageViewModel>>(msgEntityList);
            return msgModelList;
        }
    }
}
