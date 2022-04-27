using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Messenger.BLL.MessageImages;

namespace Messenger.BLL.Validators.MessageImages
{
    public class MessageImageCreateModelValidator : AbstractValidator<MessageImageCreateModel>
    {
        public MessageImageCreateModelValidator() 
        {
            RuleFor(x => x.Path).NotEmpty();
            RuleFor(x => x.MessageId).NotNull();
        }
    }
}
