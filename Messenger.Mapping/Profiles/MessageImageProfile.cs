using AutoMapper;
using Messenger.BLL.MessageImages;
using Messenger.DAL.Entities;

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
