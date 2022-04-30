using Messenger.BLL.Messages;
using Messenger.WEB.SignalR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Messenger.WEB.SignalR
{
    public class ChatHub : Hub
    {
        public void ConnectGroup(string roomName)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            Clients.Group(roomName).SendAsync("ConnectionMessage", "User connected");
        }
        public void Disconnect(string roomName)
        {
            Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            Clients.Group(roomName).SendAsync("ConnectionMessage", "User disconnected");
        }
        public void Send(string roomName, MessageSignalR message)
        {
            Clients.Group(roomName).SendAsync("SendMessage", message);
        }
    }
}
