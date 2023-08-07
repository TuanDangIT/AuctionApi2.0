using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApi.Models.Validators
{
    public class CreateAuctionDtoValidator : AbstractValidator<CreateAuctionDto>
    {
        private readonly AuctionDbContext _context;

        public CreateAuctionDtoValidator(AuctionDbContext context)
        {
            _context = context;
            RuleFor(a => a.Title).MaximumLength(20).NotEmpty();
            RuleFor(a => a.Price).NotEmpty();
            RuleFor(a => a.CategoryId).NotEmpty();
            RuleFor(a => a.Description).NotEmpty();
            RuleFor(a => a.CategoryId).Custom((value, context) =>
            {
                var categories = _context.Categories.ToList();
                if (!categories.Any(category => category.Id.Equals(value)))
                {
                    context.AddFailure("Category", $"Category is incorrect");
                }
            }).NotEmpty();
        }
    }
}
