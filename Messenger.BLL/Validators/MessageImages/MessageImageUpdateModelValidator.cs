using FluentValidation;
using Messenger.BLL.MessageImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
