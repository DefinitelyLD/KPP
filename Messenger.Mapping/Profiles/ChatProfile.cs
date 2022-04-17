using AutoMapper;
using Messenger.BLL.CreateModels;
using Messenger.BLL.UpdateModels;
using Messenger.BLL.ViewModels;
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
            CreateMap<ChatCreateModel, Chat>();
            CreateMap<ChatCreateModel, Chat>().ReverseMap();
            CreateMap<ChatUpdateModel, Chat>();
            CreateMap<ChatUpdateModel, Chat>().ReverseMap();
            CreateMap<ChatViewModel, Chat>();
            CreateMap<ChatViewModel, Chat>().ReverseMap();
        }
    }
}
