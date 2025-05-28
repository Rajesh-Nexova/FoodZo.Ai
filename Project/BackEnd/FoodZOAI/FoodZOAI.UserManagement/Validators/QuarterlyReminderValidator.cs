using FluentValidation;
using FoodZOAI.UserManagement.Models;

public class QuarterlyReminderValidator : AbstractValidator<QuarterlyReminderDTO>
{
    public QuarterlyReminderValidator()
    {
        RuleFor(x => x.ReminderId)
            .GreaterThan(0).WithMessage("ReminderId must be a positive number.");

        RuleFor(x => x.Day)
            .InclusiveBetween(1, 31).WithMessage("Day must be between 1 and 31.");

        RuleFor(x => x.Month)
            .InclusiveBetween(1, 12).WithMessage("Month must be between 1 and 12.");

        RuleFor(x => x.Quarter)
            .InclusiveBetween(1, 4).WithMessage("Quarter must be between 1 and 4.");

        RuleFor(x => x.CreatedAt)
            .NotEmpty().WithMessage("CreatedAt is required.");

        RuleFor(x => x.UpdatedAt)
            .GreaterThan(x => x.CreatedAt.GetValueOrDefault())
            .When(x => x.UpdatedAt.HasValue && x.CreatedAt.HasValue)
            .WithMessage("UpdatedAt must be after CreatedAt.");
    }
}
