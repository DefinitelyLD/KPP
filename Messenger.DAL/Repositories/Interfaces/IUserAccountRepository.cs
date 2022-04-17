using Messenger.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DAL.Repositories.Interfaces
{
    internal interface IUserAccountRepository: IRepository<UserAccount, int>
    {
    }
}
