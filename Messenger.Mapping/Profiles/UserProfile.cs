using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Messenger.BLL.Users;
using Messenger.DAL.Entities;

namespace Messenger.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateModel, User>();
            CreateMap<UserCreateModel, User>().ReverseMap();
            CreateMap<UserUpdateModel, User>();
            CreateMap<UserUpdateModel, User>().ReverseMap();
            CreateMap<UserViewModel, User>();
            CreateMap<UserViewModel, User>().ReverseMap();
        }
    }
}