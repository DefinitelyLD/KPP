using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Messenger.BLL.Validators
{
    public class FileValidator : AbstractValidator<IFormFile>
    {
        public FileValidator()
        {
            RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(50000000).WithMessage("File is too big");
        }
    }
}
