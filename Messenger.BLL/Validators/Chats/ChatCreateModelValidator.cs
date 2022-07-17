using FluentValidation;
using Messenger.BLL.Chats;
using Messenger.BLL.Validators.Files;

namespace Messenger.BLL.Validators.Chats
{
    public class ChatCreateModelValidator : AbstractValidator<ChatCreateModel>
    {
        public ChatCreateModelValidator() 
        {
            RuleFor(x => x.Topic).NotEmpty().MaximumLength(100);
            RuleFor(x => x.File).SetValidator(new ImageValidator());
        }
    }
}
