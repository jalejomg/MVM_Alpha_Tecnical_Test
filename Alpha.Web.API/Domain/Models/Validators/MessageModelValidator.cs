using FluentValidation;

namespace Alpha.Web.API.Domain.Models.Validators
{
    public class MessageModelValidator : AbstractValidator<MessageModel>
    {
        public MessageModelValidator()
        {
            RuleFor(message => message.Body)
                .NotEmpty().WithMessage("Body property can't be empty");

            RuleFor(message => message.TypeCode)
               .Must(type => type >= 0 && type <= 1).WithMessage("TypeCode property value just can be 0 or 1");
        }
    }
}
