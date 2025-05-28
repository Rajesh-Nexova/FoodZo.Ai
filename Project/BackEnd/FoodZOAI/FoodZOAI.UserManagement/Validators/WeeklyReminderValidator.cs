using FluentValidation;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

public class WeeklyReminderValidator : AbstractValidator<WeeklyReminderDTO>
{
    public WeeklyReminderValidator()
    {
        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Subject is required.")
            .MaximumLength(200).WithMessage("Subject must be at most 200 characters.");

        RuleFor(x => x.Message)
            .MaximumLength(1000).WithMessage("Message must be at most 1000 characters.")
            .When(x => !string.IsNullOrEmpty(x.Message));

        RuleFor(x => x.Frequency)
            .NotEmpty().WithMessage("Frequency is required.")
            .Must(f => new[] { "Weekly", "BiWeekly", "Monthly" }.Contains(f))
            .WithMessage("Frequency must be one of: Weekly, BiWeekly, or Monthly.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("StartDate is required.")
            .LessThanOrEqualTo(DateTime.Today.AddYears(10)).WithMessage("StartDate is too far in the future.");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate).WithMessage("EndDate must be after StartDate.")
            .When(x => x.EndDate.HasValue);

        RuleFor(x => x.DayOfWeek)
            .Must(d => string.IsNullOrEmpty(d) ||
                       new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" }.Contains(d))
            .WithMessage("DayOfWeek must be a valid weekday.");

        RuleFor(x => x.CreatedAt)
            .NotEmpty().WithMessage("CreatedAt is required.")
            .When(x => x.Id == 0); // on creation

        RuleFor(x => x.ModifiedAt)
            .NotEmpty().WithMessage("ModifiedAt is required.")
            .When(x => x.Id > 0); // on update

        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("IsActive must be specified.");

        RuleFor(x => x.CreatedByUser)
            .MaximumLength(100).WithMessage("CreatedByUser must be at most 100 characters.");

        RuleFor(x => x.ModifiedByUser)
            .MaximumLength(100).WithMessage("ModifiedByUser must be at most 100 characters.");

        RuleFor(x => x.DeletedByUser)
            .MaximumLength(100).WithMessage("DeletedByUser must be at most 100 characters.");

        RuleFor(x => x.DeletedAt)
            .Must(date => !date.HasValue || date.Value > DateTime.MinValue)
            .WithMessage("DeletedAt must be a valid date if provided.");
    }
}
