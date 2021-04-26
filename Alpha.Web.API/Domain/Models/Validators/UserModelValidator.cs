using FluentValidation;

namespace Alpha.Web.API.Domain.Models.Validators
{
    public class UserModelValidator : AbstractValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(model => model.Name)
                .NotEmpty().WithMessage("Name parameter can't be empty")
                .MaximumLength(50).WithMessage("Name parameter must have less than 50 characters");

            RuleFor(model => model.Email)
                .NotEmpty().WithMessage("Email parameter can't be empty")
                .MaximumLength(100).WithMessage("Email parameter must have less than 50 characters");
        }
    }
}
