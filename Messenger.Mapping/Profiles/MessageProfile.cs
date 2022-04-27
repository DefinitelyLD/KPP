using AutoMapper;
using Messenger.BLL.Messages;
using Messenger.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Mapping.Profiles
{
    public class MessageProfile: Profile
    {
        public MessageProfile()
        {
            CreateMap<MessageCreateModel, Message>().ReverseMap();
            CreateMap<MessageUpdateModel, Message>().ReverseMap();
            CreateMap<MessageViewModel, Message>().ReverseMap();
        }
    }
}
