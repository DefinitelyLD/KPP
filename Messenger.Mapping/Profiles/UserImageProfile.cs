using AutoMapper;
using Messenger.BLL.Models.UserImages;
using Messenger.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Mapping.Profiles
{
    public class UserImageProfile : Profile
    {
        public UserImageProfile()
        {
            CreateMap<UserImageCreateModel, UserImage>().ReverseMap();
            CreateMap<UserImageUpdateModel, UserImage>().ReverseMap();
            CreateMap<UserImageViewModel, UserImage>().ReverseMap();
        }
    }

}
