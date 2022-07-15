using FluentValidation;
using Messenger.BLL.Messages;

namespace Messenger.BLL.Validators.Messages
{
    public class MessageCreateModelValidator : AbstractValidator<MessageCreateModel>
    {
        public MessageCreateModelValidator() 
        {
            RuleFor(x => x.ChatId).NotNull();
            RuleForEach(x => x.Files).SetValidator(new FileValidator());
            RuleFor(x => x.Text).MaximumLength(3000);
        }
    }
}
