﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Users
{
    public class UserCreateModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}