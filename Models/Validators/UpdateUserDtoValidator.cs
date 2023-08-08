using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApi.Models.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(u => u.Password).Equal(u => u.ConfirmPassword).WithMessage("Password has to be equal to ConfirmPassword");
            RuleFor(u => u.Email).EmailAddress();
        }
    }
}
