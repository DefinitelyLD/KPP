using FluentValidation;
using Messenger.BLL.Chats;

namespace Messenger.BLL.Validators.Chats
{
    public class ChatCreateModelValidator : AbstractValidator<ChatCreateModel>
    {
        public ChatCreateModelValidator() 
        {
            RuleFor(x => x.Topic).NotEmpty().MaximumLength(100);
        }
    }
}
