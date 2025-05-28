using FluentValidation;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Validators
{
    public class OneTimeReminderValidator : AbstractValidator<OneTimeReminderDTO>
    {
        public OneTimeReminderValidator()
        {
            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required.")
                .MaximumLength(255).WithMessage("Message must not exceed 255 characters.");

            RuleFor(x => x.ReminderDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Reminder date must be in the future.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive must be specified.");
        }
    }
}
