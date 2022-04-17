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
    public class ChatroomManager: IChatroomManager
    {
        private readonly IMapper _mapper;
        private readonly IChatsRepository _chatsRep;

        public ChatroomManager(IMapper mapper, IChatsRepository chatsRep)
        {
            _mapper = mapper;
            _chatsRep = chatsRep;
        }

        public ChatCreateModel CreateChatroom(ChatCreateModel chat)
        {
            var chatEntity = _mapper.Map<Chat>(chat);
            return _mapper.Map<ChatCreateModel>(_chatsRep.Create(chatEntity));
        }
    }
}
