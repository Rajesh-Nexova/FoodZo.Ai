using FluentValidation;
using FoodZOAI.UserManagement.Models;

public class DailyReminderValidator : AbstractValidator<DailyReminderDTO>
{
    public DailyReminderValidator()
    {
        RuleFor(x => x.ReminderId)
            .GreaterThan(0).WithMessage("ReminderId must be a positive number.");

        RuleFor(x => x.DayOfWeek)
            .Must(d => string.IsNullOrEmpty(d) ||
                       new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" }.Contains(d))
            .WithMessage("DayOfWeek must be a valid day (Monday to Sunday).");

        RuleFor(x => x.CreatedAt)
            .NotEmpty().WithMessage("CreatedAt is required.")
            .When(x => x.Id == 0); // Required when creating

        RuleFor(x => x.UpdatedAt)
            .NotEmpty().WithMessage("UpdatedAt is required.")
            .When(x => x.Id > 0); // Required when updating

        RuleFor(x => x.DeletedAt)
            .Must(date => !date.HasValue || date.Value > DateTime.MinValue)
            .WithMessage("DeletedAt must be a valid date if provided.");

        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("IsActive must be specified.");
    }
}
