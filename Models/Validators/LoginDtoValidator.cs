using FluentValidation;

namespace AuctionApi.Models.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(l => l.Email).EmailAddress().NotEmpty();
            RuleFor(l => l.Password).NotEmpty();
        }
    }
}
