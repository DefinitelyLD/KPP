using AutoMapper;
using Messenger.BLL.MessageImages;
using Messenger.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Mapping.Profiles
{
    public class MessageImageProfile: Profile 
    {
        public MessageImageProfile()
        {
            CreateMap<MessageImageCreateModel, MessageImage>().ReverseMap();
            CreateMap<MessageImageUpdateModel, MessageImage>().ReverseMap();
            CreateMap<MessageImageViewModel, MessageImage>().ReverseMap();
        }
    }
}
