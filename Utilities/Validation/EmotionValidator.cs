using FluentValidation;
using Wellness_Tracker.Models.DTOs.CreateDTOs;

namespace Wellness_Tracker.Utilities.Validation;

public class EmotionValidator : AbstractValidator<EmotionEntryCreateDto>
{
    public EmotionValidator()
    {
        RuleFor(x => x.GeneralMood)
            .NotEmpty()
            .InclusiveBetween(1, 5);
        RuleFor(x => x.Wins)
            .MaximumLength(4000)
            .When(x => !string.IsNullOrEmpty(x.Wins));
        RuleFor(x => x.Losses)
            .MaximumLength(4000)
            .When(x => !string.IsNullOrEmpty(x.Losses));
        RuleFor(x => x.Comments)
            .MaximumLength(4000)
            .When(x => !string.IsNullOrEmpty(x.Comments));
        RuleFor(x => x.GeneralMoodDateTime)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.UtcNow);
    }
}