using AutoMapper;
using Messenger.BLL.Chats;
using Messenger.DAL.Entities;

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
