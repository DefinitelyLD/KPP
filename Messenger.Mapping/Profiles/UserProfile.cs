using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Messenger.BLL.Models;
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
            CreateMap<UserChangePasswordModel, User>();
            CreateMap<UserChangePasswordModel, User>().ReverseMap();
            CreateMap<UserLoginModel, User>();
            CreateMap<UserLoginModel, User>().ReverseMap();
        }
    }
}