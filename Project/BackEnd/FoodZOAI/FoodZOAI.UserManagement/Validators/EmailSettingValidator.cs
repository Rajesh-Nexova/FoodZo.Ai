using FluentValidation;
using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Validators
{
    public class EmailSettingValidator : AbstractValidator<EmailSettingDTO>
    {
        public EmailSettingValidator()
        {
            RuleFor(x => x.Host)
                .NotEmpty().WithMessage("SMTP Host is required.")
                .Must(BeAValidHost).WithMessage("Invalid SMTP host format.");

            RuleFor(x => x.Port)
                .InclusiveBetween(1, 65535).WithMessage("Port must be between 1 and 65535.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("SMTP username (email) is required.")
                .EmailAddress().WithMessage("SMTP username must be a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("SMTP password is required.");

            RuleFor(x => x.CreatedByUser)
                .MaximumLength(100).WithMessage("CreatedByUser must be 100 characters or less.")
                .When(x => !string.IsNullOrWhiteSpace(x.CreatedByUser));

            RuleFor(x => x.ModifiedByUser)
                .MaximumLength(100).WithMessage("ModifiedByUser must be 100 characters or less.")
                .When(x => !string.IsNullOrWhiteSpace(x.ModifiedByUser));

            RuleFor(x => x.DeletedByUser)
                .MaximumLength(100).WithMessage("DeletedByUser must be 100 characters or less.")
                .When(x => !string.IsNullOrWhiteSpace(x.DeletedByUser));
        }

        private bool BeAValidHost(string host)
        {
            return Uri.CheckHostName(host) != UriHostNameType.Unknown;
        }
    }
}
