using FluentValidation;

namespace Alpha.Web.API.Domain.Models.Validators
{
    public class MessageModelValidator : AbstractValidator<MessageModel>
    {
        public MessageModelValidator()
        {
            RuleFor(message => message.Addressee)
                .NotNull().WithMessage("Addressee property can't be null");

            RuleFor(message => message.Sender)
                .NotNull().WithMessage("Sender property can't be null");

            RuleFor(message => message.Body)
                .NotEmpty().WithMessage("Body property can't be empty");

            RuleFor(message => message.Type)
               .Must(type => type >= 0 && type <= 1).WithMessage("Type property value just can be 0 or 1");
        }
    }
}
