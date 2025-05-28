using FluentValidation;
using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Validators
{
    public class SendEmailValidator : AbstractValidator<SendEmailDTO>
    {
        public SendEmailValidator()
        {
            RuleFor(x => x.To)
               .NotNull().WithMessage("To field must not be null.")
               .Must(list => list != null && list.Any()).WithMessage("At least one recipient (To) is required.")
               .ForEach(emailRule => emailRule.EmailAddress().WithMessage("Invalid email format in To list."));

            RuleForEach(x => x.Cc)
                .EmailAddress().WithMessage("Invalid email format in Cc list.")
                .When(x => x.Cc != null && x.Cc.Any());

            RuleForEach(x => x.Bcc)
                .EmailAddress().WithMessage("Invalid email format in Bcc list.")
                .When(x => x.Bcc != null && x.Bcc.Any());

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Subject is required.")
                .MaximumLength(200).WithMessage("Subject cannot be longer than 200 characters.");

            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Body is required.");

            RuleFor(x => x.FromName)
                .MaximumLength(100).WithMessage("FromName cannot be longer than 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FromName));

            RuleFor(x => x.Attachments)
                .Must(attachments => attachments == null || attachments.All(file => file.Length <= 10 * 1024 * 1024)) // max 10 MB per file
                .WithMessage("Each attachment must be 10 MB or less.")
                .When(x => x.Attachments != null && x.Attachments.Any());

            RuleFor(x => x.TemplateId)
                .GreaterThan(0).WithMessage("TemplateId must be a positive integer.")
                .When(x => x.TemplateId.HasValue);
        }
    }
}
