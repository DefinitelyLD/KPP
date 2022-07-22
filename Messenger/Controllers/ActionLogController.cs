using Microsoft.AspNetCore.Mvc;
using Messenger.BLL.Managers;
using System.Collections.Generic;
using System;
using Messenger.BLL.Managers.Interfaces;
using Messenger.BLL.Models.ActionLogs;
using Microsoft.AspNetCore.Authorization;
using Messenger.WEB.Roles;

namespace Messenger.WEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ActionLogController : Controller
    {
        private readonly IActionLogManager _logger;

        public ActionLogController(IActionLogManager actionLogManager)
        {
            _logger = actionLogManager;
        }
        
        [HttpGet]
        [Authorize(Roles = RolesConstants.Admin)]
        public IEnumerable<ActionLogViewModel> GetAllLogs(DateTime? date = null, string userId = null)
        {
            return _logger.GetAllLogs(date, userId);
        }
    }
}
