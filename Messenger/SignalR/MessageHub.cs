using Messenger.BLL.Messages;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;
using System.Threading.Tasks;

namespace Messenger.WEB.SignalR
{
    [SignalRHub]
    public class MessageHub : Hub
    {
        public async Task Send(MessageCreateModel message)
        {
            await this.Clients.All.SendAsync("SendMessage", message);
        }
    }
}
