using FluentValidation;
using Wellness_Tracker.Models.DTOs.CreateDTOs;

namespace Wellness_Tracker.Utilities.Validation;

public class NoteValidator :AbstractValidator<NoteCreateDto>
{
    public NoteValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(4000)
            .When(x => !string.IsNullOrEmpty(x.Title));
        RuleFor(x => x.Content)
            .MaximumLength(4000)
            .When(x => !string.IsNullOrEmpty(x.Content));
    }
}