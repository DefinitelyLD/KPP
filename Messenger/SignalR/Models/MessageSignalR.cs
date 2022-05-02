using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Messenger.WEB.SignalR.Models
{
    public class MessageSignalR
    {
        public string UserName { get; set; }
        public string Text { get; set; }
    }
}
