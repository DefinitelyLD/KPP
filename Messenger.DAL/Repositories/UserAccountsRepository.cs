using Messenger.DAL.Context;
using Messenger.DAL.Entities;
using Messenger.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DAL.Repositories
{
    public class UserAccountsRepository : BaseRepository<UserAccount, int>, IUserAccountsRepository
    {
        public UserAccountsRepository(AppDbContext context) : base(context)
        {
        }
    }
}
