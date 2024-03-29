﻿using FluentValidation;
using Messenger.BLL.Messages;

namespace Messenger.BLL.Validators.Messages
{
    public class MessageViewModelValidator: AbstractValidator<MessageViewModel>
    {
        public MessageViewModelValidator() 
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.User).NotNull();
            RuleFor(x => x.Text).MaximumLength(3000);
        }
    }
}
