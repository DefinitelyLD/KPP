using Messenger.DAL.Context;
using Messenger.DAL.Entities;
using Messenger.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DAL.Repositories
{
    public class MessagesRepository : BaseMongoRepository<Message>, IMessagesRepository
    {
        public MessagesRepository(DbContext context) : base(context)
        {
        }
    }
}
