using Messenger.BLL.Messages;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers
{
    public interface IMessageManager
    {
        public Task<MessageViewModel> SendMessage(MessageCreateModel messageModel, string userId);
        public Task<MessageViewModel> EditMessage(MessageUpdateModel messageModel, string userId);
        public Task<bool> DeleteMessage(int messageId, string userId);
        public MessageViewModel GetMessage(int messageId);
        public IEnumerable<MessageViewModel> GetMessagesFromChat(int chatId, string userId, DateTime? date = null);
    }
}
