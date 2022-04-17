﻿using Messenger.BLL.CreateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.ViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public UserCreateModel User { get; set; }
        public ICollection<MessageImageCreateModel> Images { get; set; }
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
