using FluentValidation;
using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Validators
{
    public class EmailTemplateValidator : AbstractValidator<EmailTemplateDTO>
    {
        public EmailTemplateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Template name is required.")
                .MaximumLength(100).WithMessage("Template name must be at most 100 characters.");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Subject is required.")
                .MaximumLength(200).WithMessage("Subject must be at most 200 characters.");

            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Email body is required.");

            RuleFor(x => x.CreatedByUser)
                .MaximumLength(50).WithMessage("CreatedByUser must be at most 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.CreatedByUser));

            RuleFor(x => x.ModifiedByUser)
                .MaximumLength(50).WithMessage("ModifiedByUser must be at most 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.ModifiedByUser));

            RuleFor(x => x.DeletedByUser)
                .MaximumLength(50).WithMessage("DeletedByUser must be at most 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.DeletedByUser));
        }
    }
}
