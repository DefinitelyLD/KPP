using FluentValidation;
using Messenger.BLL.Chats;

namespace Messenger.BLL.Validators.Chats
{
    public class ChatUpdateModelValidator : AbstractValidator<ChatUpdateModel>
    {
        public ChatUpdateModelValidator() 
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Topic).NotEmpty().MaximumLength(100);
        }
    }
}
