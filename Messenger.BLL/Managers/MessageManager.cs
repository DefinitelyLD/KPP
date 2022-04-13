using AutoMapper;
using Messenger.BLL.Models;
using Messenger.DAL.Entities;
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
        
        public MessageManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        private Message MappMessage(MessageModel msg)
        {
            return _mapper.Map<Message>(msg);
        }

        public Message ManagerSendMessage (MessageModel msg)
        {
            return MappMessage(msg);
        }

        public void ManagerEditMessage(MessageModel msg)
        {
            throw new NotImplementedException();
        }

        public void ManagerDeleteMessage(MessageModel msg)
        {
            throw new NotImplementedException();
        }

        public void ManagerGetMessage(MessageModel msg)
        {
            throw new NotImplementedException();
        }
    }
}
