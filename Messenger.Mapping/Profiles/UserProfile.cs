using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Messenger.BLL.Models;
using Messenger.DAL.Entities;

namespace Messenger.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, User>();
            CreateMap<UserModel, User>().ReverseMap();
        }
    }
}