using AutoMapper;
using Messenger.BLL.Models;
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
            CreateMap<MessageCreateModel, Message>();
            CreateMap<MessageCreateModel, Message>().ReverseMap();
        }
    }
}
