using FluentValidation;
using Messenger.BLL.Messages;
using Messenger.BLL.Validators.Files;

namespace Messenger.BLL.Validators.Messages
{
    public class MessageUpdateModelValidator : AbstractValidator<MessageUpdateModel>
    {
        public MessageUpdateModelValidator() 
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Text).MaximumLength(3000);
            RuleFor(x => x.File).SetValidator(new FileValidator());
        }
    }
}
