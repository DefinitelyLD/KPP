using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Messenger.BLL.Chats;

namespace Messenger.BLL.Validators.Chats
{
    public class ChatCreateModelValidator : AbstractValidator<ChatCreateModel>
    {
        public ChatCreateModelValidator() 
        {
            RuleFor(x => x.Topic).NotEmpty().MaximumLength(100);
            RuleFor(x => x.UserId).NotNull();
        }
    }
}
