using Messenger.DAL.Context;
using Messenger.DAL.Entities;
using Messenger.DAL.Repositories.Interfaces;

namespace Messenger.DAL.Repositories
{
    public class ActionLogsRepository : BaseRepository<ActionLog, int>, IActionLogsRepository
    {
        public ActionLogsRepository(AppDbContext context) : base(context)
        {

        }
    }
}
