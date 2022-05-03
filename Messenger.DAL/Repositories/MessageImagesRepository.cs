﻿using Messenger.DAL.Context;
using Messenger.DAL.Entities;
using Messenger.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DAL.Repositories
{
    public class MessageImagesRepository : BaseRepository<MessageImage, int>, IMessageImagesRepository
    {
        public MessageImagesRepository(AppDbContext context) : base(context)
        {

        }
    }
}
