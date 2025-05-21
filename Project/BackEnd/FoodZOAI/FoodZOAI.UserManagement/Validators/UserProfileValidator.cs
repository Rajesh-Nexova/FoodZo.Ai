using FluentValidation;
using FoodZOAI.UserManagement.DTOs;

public class UserProfileValidator : AbstractValidator<UserProfileDTO>
{
    public UserProfileValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId must be a positive number.");

        RuleFor(x => x.Bio)
            .MaximumLength(500).WithMessage("Bio must be at most 500 characters.")
            .When(x => !string.IsNullOrEmpty(x.Bio));

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Date of birth must be in the past.")
            .When(x => x.DateOfBirth.HasValue);

        RuleFor(x => x.Gender)
            .Must(g => string.IsNullOrEmpty(g) || new[] { "Male", "Female", "Other" }.Contains(g))
            .WithMessage("Gender must be Male, Female, or Other.");

        RuleFor(x => x.Address)
            .MaximumLength(200).WithMessage("Address must be at most 200 characters.");

        RuleFor(x => x.City)
            .MaximumLength(100).WithMessage("City must be at most 100 characters.");

        RuleFor(x => x.StateProvince)
            .MaximumLength(100).WithMessage("State/Province must be at most 100 characters.");

        RuleFor(x => x.PostalCode)
            .MaximumLength(20).WithMessage("Postal Code must be at most 20 characters.");

        RuleFor(x => x.Country)
            .MaximumLength(100).WithMessage("Country must be at most 100 characters.");

        RuleFor(x => x.Timezone)
            .MaximumLength(100).WithMessage("Timezone must be at most 100 characters.");

        RuleFor(x => x.Language)
            .MaximumLength(50).WithMessage("Language must be at most 50 characters.");

        RuleFor(x => x.NotificationPreferences)
            .MaximumLength(200).WithMessage("Notification Preferences must be at most 200 characters.");

        RuleFor(x => x.PrivacySettings)
            .MaximumLength(200).WithMessage("Privacy Settings must be at most 200 characters.");

        RuleFor(x => x.CustomFields)
            .MaximumLength(1000).WithMessage("Custom fields must be at most 1000 characters.");

        RuleFor(x => x.CreatedAt)
            .NotEmpty().WithMessage("CreatedAt is required.")
            .When(x => x.Id == 0); // Require CreatedAt on creation

        RuleFor(x => x.UpdatedAt)
            .NotEmpty().WithMessage("UpdatedAt is required.")
            .When(x => x.Id > 0); // Require UpdatedAt on update
    }
}
