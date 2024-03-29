﻿using AutoMapper;
using Messenger.BLL.Users;
using Messenger.DAL.Entities;

namespace Messenger.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateModel, User>().ReverseMap();
            CreateMap<UserChangePasswordModel, User>().ReverseMap();
            CreateMap<UserLoginModel, User>().ReverseMap();
            CreateMap<UserViewModel, User>().ReverseMap();
            CreateMap<UserUpdateModel, User>().ReverseMap();
        }
    }
}