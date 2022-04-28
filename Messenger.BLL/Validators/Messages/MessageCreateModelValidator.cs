using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Messenger.BLL.Messages;

namespace Messenger.BLL.Validators.Messages
{
    public class MessageCreateModelValidator : AbstractValidator<MessageCreateModel>
    {
        public MessageCreateModelValidator() 
        {
            RuleFor(x => x.ChatId).NotNull();
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.Text).MaximumLength(3000);
        }
    }
}
