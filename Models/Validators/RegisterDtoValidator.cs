using FluentValidation;
using System.Runtime.CompilerServices;

namespace AuctionApi.Models.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterDtoValidator(AuctionDbContext dbContext) 
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress()
                .Custom((value, context) =>
                {
                    if(dbContext.Users.Any(u => u.Email == value))
                    {
                        context.AddFailure("Email", "The email is taken.");
                    }
                });
            RuleFor(r => r.FirstName).NotEmpty();
            RuleFor(r => r.LastName).NotEmpty();
            RuleFor(r => r.Password).MinimumLength(6).NotEmpty();
            RuleFor(r => r.ConfirmPassword).Equal(r => r.Password).NotEmpty();
        }
    }
}
