using AutoMapper;
using Messenger.BLL.UserAccounts;
using Messenger.DAL.Entities;

namespace Messenger.Mapping.Profiles
{
    public class UserAccountProfile: Profile
    {
        public UserAccountProfile()
        {
            CreateMap<UserAccountCreateModel, UserAccount>().ReverseMap();
            CreateMap<UserAccountUpdateModel, UserAccount>().ReverseMap();
            CreateMap<UserAccountViewModel, UserAccount>().ReverseMap();
        }
    }
}
