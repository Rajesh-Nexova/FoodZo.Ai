using FluentValidation;
using FoodZOAI.UserManagement.DTOs;

public class UserValidation : AbstractValidator<UserDTO>
{
    public UserValidation()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

        RuleFor(x => x.Salt)
            .NotEmpty().WithMessage("Salt is required.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50);

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number format.");

        RuleFor(x => x.AvatarUrl)
            .MaximumLength(255).When(x => !string.IsNullOrEmpty(x.AvatarUrl));

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.");

        RuleFor(x => x.FailedLoginAttempts)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.TwoFactorSecret)
            .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.TwoFactorSecret));

        RuleFor(x => x.CreatedAt)
            .NotEmpty().WithMessage("CreatedAt is required.");
    }
}
