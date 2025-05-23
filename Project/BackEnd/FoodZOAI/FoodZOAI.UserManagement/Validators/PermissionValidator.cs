using FluentValidation;
using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Validators
{
    public class PermissionValidator : AbstractValidator<PermissionDTO>
    {
        public PermissionValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100);

            RuleFor(p => p.Slug)
                .NotEmpty().WithMessage("Slug is required.")
                .MaximumLength(100);

            RuleFor(p => p.Description)
                .MaximumLength(250);

            RuleFor(p => p.Module)
                .MaximumLength(100);

            RuleFor(p => p.Action)
                .MaximumLength(100);

            RuleFor(p => p.Resource)
                .MaximumLength(100);
        }
    }
}
