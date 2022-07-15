using FluentValidation;
using Messenger.BLL.Chats;

namespace Messenger.BLL.Validators.Chats
{
    public class ChatInviteModelValidator : AbstractValidator<ChatInviteModel>
    {
        public ChatInviteModelValidator() 
        {
            RuleFor(x => x.ChatId).NotNull();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
