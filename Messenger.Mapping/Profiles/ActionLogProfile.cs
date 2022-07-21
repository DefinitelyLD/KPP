using AutoMapper;
using Messenger.BLL.Models.ActionLogs;
using Messenger.DAL.Entities;

namespace Messenger.Mapping.Profiles
{
    public class ActionLogProfile : Profile
    {
        public ActionLogProfile()
        {
            CreateMap<ActionLogViewModel, ActionLog>().ReverseMap();
        }
    }
}