using AutoMapper;
using Messenger.BLL.Managers.Interfaces;
using Messenger.BLL.Models.ActionLogs;
using Messenger.DAL.Entities;
using Messenger.DAL.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers
{
    public class ActionLogManager : IActionLogManager
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ActionLogManager(IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionLogViewModel> CreateLog(string actionName, string userId)
        {
            ActionLog actionLogEntity = new()
            {
                ActionName = actionName,
                UserId = userId
            };

            var result = await _unitOfWork.ActionLogs.CreateAsync(actionLogEntity);
            var model = _mapper.Map<ActionLogViewModel>(result);

            return model;
        }

        public IEnumerable<ActionLogViewModel> GetAllLogs(DateTime? date = null, string userId = null)
        {
            var logEntityList = _unitOfWork.ActionLogs
                .GetAll()
                .Where(u => (date == null || u.Time.Date == date.Value.Date) && 
                (string.IsNullOrEmpty(userId) || u.UserId == userId))
                .OrderByDescending(d => d.Time)
                .ToList();

            var logModelList = _mapper.Map<IEnumerable<ActionLogViewModel>>(logEntityList);

            return logModelList;
        }
    }
}
