using AutoMapper;
using Messenger.BLL.Messages;
using Messenger.DAL.Entities;

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
