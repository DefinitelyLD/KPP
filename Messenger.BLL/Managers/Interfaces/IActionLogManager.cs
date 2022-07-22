using Messenger.BLL.Models.ActionLogs;
using Messenger.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers.Interfaces
{
    public interface IActionLogManager
    {
        public Task<ActionLogViewModel> CreateLog(string ActionName, string userId);
        public IEnumerable<ActionLogViewModel> GetAllLogs(DateTime? date = null, string userId = null);
    }
}
