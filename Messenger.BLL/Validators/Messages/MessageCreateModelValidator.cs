using FluentValidation;
using Messenger.BLL.Messages;
using Messenger.BLL.Validators.Files;

namespace Messenger.BLL.Validators.Messages
{
    public class MessageCreateModelValidator : AbstractValidator<MessageCreateModel>
    {
        public MessageCreateModelValidator() 
        {
            RuleFor(x => x.ChatId).NotNull();
            RuleForEach(x => x.Files).SetValidator(new ImageValidator());
            RuleFor(x => x.Text).MaximumLength(3000);
        }
    }
}
