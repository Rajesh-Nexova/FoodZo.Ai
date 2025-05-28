using FluentValidation;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

public class MonthlyReminderValidator : AbstractValidator<MonthlyReminderDTO>
{
    public MonthlyReminderValidator()
    {
        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Subject is required.")
            .MaximumLength(200).WithMessage("Subject must be at most 200 characters.");

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message is required.")
            .MaximumLength(1000).WithMessage("Message must be at most 1000 characters.");

        RuleFor(x => x.Frequency)
            .NotEmpty().WithMessage("Frequency is required.")
            .Must(f => new[] { "Monthly", "BiMonthly", "Quarterly" }.Contains(f))
            .WithMessage("Frequency must be 'Monthly', 'BiMonthly', or 'Quarterly'.");

        RuleFor(x => x.DayOfMonth)
            .InclusiveBetween(1, 31).WithMessage("DayOfMonth must be between 1 and 31.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("StartDate is required.")
            .LessThanOrEqualTo(DateTime.Today.AddYears(10)).WithMessage("StartDate is too far in the future.");

        RuleFor(x => x.CreatedAt)
            .NotEmpty().WithMessage("CreatedAt is required.");

        RuleFor(x => x.UpdatedAt)
            .GreaterThan(x => x.CreatedAt.GetValueOrDefault())
            .When(x => x.UpdatedAt.HasValue && x.CreatedAt.HasValue)
            .WithMessage("UpdatedAt must be after CreatedAt.");

        RuleFor(x => x.CreatedByUser)
            .NotEmpty().WithMessage("CreatedByUser is required.")
            .MaximumLength(100).WithMessage("CreatedByUser must be at most 100 characters.");
    }
}
