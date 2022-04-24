using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Messenger.BLL.Chats;

namespace Messenger.BLL.Validators.Chats
{
    public class ChatViewModelValidator : AbstractValidator<ChatViewModel>
    {
        public ChatViewModelValidator() 
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Topic).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Password).MaximumLength(20);
            RuleFor(x => x.Users).NotNull();
        }
    }
}
