using FluentValidation;
using Messenger.BLL.MessageImages;

namespace Messenger.BLL.Validators.MessageImages
{
    public class MessageImageViewModelValidator : AbstractValidator<MessageImageViewModel>
    {
        public MessageImageViewModelValidator() 
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Path).NotEmpty();
            RuleFor(x => x.Message).NotNull();
        }
    }
}
