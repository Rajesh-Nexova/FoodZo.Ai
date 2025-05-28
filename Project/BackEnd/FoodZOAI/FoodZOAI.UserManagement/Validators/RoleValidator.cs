using FluentValidation;
using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Validators
{
    public class RoleValidator : AbstractValidator<RoleDTO>
    {
        public RoleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Role name is required.")
                .MaximumLength(100).WithMessage("Role name must not exceed 100 characters.");

            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Slug is required.")
                .MaximumLength(100).WithMessage("Slug must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage("Description must not exceed 250 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.Level)
                .GreaterThanOrEqualTo(0).WithMessage("Level must be a non-negative number.")
                .When(x => x.Level.HasValue);

            RuleFor(x => x.Color)
                .Matches("^#(?:[0-9a-fA-F]{3}){1,2}$").WithMessage("Color must be a valid hex code (e.g., #FFFFFF).")
                .When(x => !string.IsNullOrWhiteSpace(x.Color));
        }
    }
}
