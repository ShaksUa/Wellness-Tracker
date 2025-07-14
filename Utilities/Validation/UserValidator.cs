using FluentValidation;
using WellnessTracker.Models.DTOs.CreateDTOs;

namespace WellnessTracker.Utilities.Validation;

public class UserValidator : AbstractValidator<UserCreateDto>
{
    public UserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email));
        RuleFor(x => x.Age)
            .InclusiveBetween(1, 120);
        RuleFor(x => x.RegistrationDateTime)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.UtcNow);
    }
}