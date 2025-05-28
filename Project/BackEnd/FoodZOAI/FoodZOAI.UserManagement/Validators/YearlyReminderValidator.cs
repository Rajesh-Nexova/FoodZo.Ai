using FluentValidation;
using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Validators
{
    public class YearlyReminderValidator : AbstractValidator<YearlyReminderDTO>
    {
        public YearlyReminderValidator()
        {
            RuleFor(x => x.ReminderId).GreaterThan(0).WithMessage("ReminderId is required.");
            RuleFor(x => x.Day).InclusiveBetween(1, 31).WithMessage("Day must be between 1 and 31.");
            RuleFor(x => x.Month).InclusiveBetween(1, 12).WithMessage("Month must be between 1 and 12.");
        }
    }
}
