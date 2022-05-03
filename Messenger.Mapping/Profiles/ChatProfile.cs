using AutoMapper;
using Messenger.BLL.Chats;
using Messenger.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Mapping.Profiles
{
    public class ChatProfile: Profile
    {
        public ChatProfile()
        {
            CreateMap<ChatCreateModel, Chat>().ReverseMap();
            CreateMap<ChatUpdateModel, Chat>().ReverseMap();
            CreateMap<ChatViewModel, Chat>().ReverseMap();
        }
    }
}
