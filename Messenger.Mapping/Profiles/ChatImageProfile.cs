using AutoMapper;
using Messenger.BLL.ChatImages;
using Messenger.DAL.Entities;

namespace Messenger.Mapping.Profiles
{
    public class ChatImageProfile: Profile 
    {
        public ChatImageProfile()
        {
            CreateMap<ChatImageCreateModel, ChatImage>().ReverseMap();
            CreateMap<ChatImageUpdateModel, ChatImage>().ReverseMap();
            CreateMap<ChatImageViewModel, ChatImage>().ReverseMap();
        }
    }
}
