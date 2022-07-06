using FluentValidation;
using Messenger.BLL.MessageImages;

namespace Messenger.BLL.Validators.MessageImages
{
    public class MessageImageUpdateModelValidator : AbstractValidator<MessageImageUpdateModel>
    {
        public MessageImageUpdateModelValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Path).NotEmpty();
        }
    }
}
