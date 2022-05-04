using AutoMapper;
using Messenger.BLL.UserAccounts;
using Messenger.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
