using FluentValidation;
using Messenger.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Validators
{
    public class BaseModelValidator<TId> : AbstractValidator<BaseModel<TId>> where TId : IComparable<TId>
    {
        BaseModelValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }
}
