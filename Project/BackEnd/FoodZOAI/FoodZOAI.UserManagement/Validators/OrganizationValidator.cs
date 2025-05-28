using FluentValidation;
using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Validators
{
    public class OrganizationValidator : AbstractValidator<OrganizationDTO>
    {
        public OrganizationValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Organization name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Slug is required.")
                .MaximumLength(100).WithMessage("Slug must not exceed 100 characters.")
                .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$").WithMessage("Slug must be URL-friendly (lowercase letters, numbers, and hyphens only).");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Website)
                .MaximumLength(200).WithMessage("Website URL must not exceed 200 characters.")
                .Matches(@"^(https?:\/\/)?([\w\-]+\.)+[\w\-]+(\/[\w\-]*)*\/?$")
                .WithMessage("Website must be a valid URL.")
                .When(x => !string.IsNullOrEmpty(x.Website));

            RuleFor(x => x.LogoUrl)
                .MaximumLength(300).WithMessage("Logo URL must not exceed 300 characters.")
                .When(x => !string.IsNullOrEmpty(x.LogoUrl));

            RuleFor(x => x.Banner_Image)
                .MaximumLength(300).WithMessage("Banner image URL must not exceed 300 characters.")
                .When(x => !string.IsNullOrEmpty(x.Banner_Image));

            RuleFor(x => x.SubscriptionPlan)
                .MaximumLength(50).WithMessage("Subscription plan must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.SubscriptionPlan));

            RuleFor(x => x.MaxUsers)
                .GreaterThanOrEqualTo(0).WithMessage("MaxUsers must be 0 or more.")
                .When(x => x.MaxUsers.HasValue);

            RuleFor(x => x.Status)
                .MaximumLength(50).WithMessage("Status must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.Status));
        }

    }
}
