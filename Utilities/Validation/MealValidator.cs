using FluentValidation;
using Wellness_Tracker.Models.DTOs.CreateDTOs;

namespace Wellness_Tracker.Utilities.Validation;

public class MealValidator : AbstractValidator<MealEntryCreateDto>
{
    public MealValidator()
    {
        RuleFor(x => x.Breakfast)
            .MaximumLength(4000)
            .When(x => !string.IsNullOrEmpty(x.Breakfast));
        RuleFor(x => x.BreakfastDateTime)
            .NotEmpty()
            .When(x => !string.IsNullOrEmpty(x.Breakfast))
            .LessThanOrEqualTo(DateTime.UtcNow);
        RuleFor(x => x.Lunch)
            .MaximumLength(4000)
            .When(x => !string.IsNullOrEmpty(x.Lunch));
        RuleFor(x => x.LunchDateTime)
            .NotEmpty()
            .When(x => !string.IsNullOrEmpty(x.Lunch))
            .LessThanOrEqualTo(DateTime.UtcNow);
        RuleFor(x => x.Dinner)
            .MaximumLength(4000)
            .When(x => !string.IsNullOrEmpty(x.Dinner));
        RuleFor(x => x.DinnerDateTime)
            .NotEmpty()
            .When(x => !string.IsNullOrEmpty(x.Dinner))
            .LessThanOrEqualTo(DateTime.UtcNow);
        RuleFor(x => x.Supper)
            .MaximumLength(4000)
            .When(x => !string.IsNullOrEmpty(x.Supper));
        RuleFor(x => x.SupperDateTime)
            .NotEmpty()
            .When(x => !string.IsNullOrEmpty(x.Supper))
            .LessThanOrEqualTo(DateTime.UtcNow);
        RuleFor(x => x.OtherMealEntry)
            .MaximumLength(4000)
            .When(x => !string.IsNullOrEmpty(x.OtherMealEntry));
        RuleFor(x => x.OtherMealEntryDateTime)
            .NotEmpty()
            .When(x => !string.IsNullOrEmpty(x.OtherMealEntry))
            .LessThanOrEqualTo(DateTime.UtcNow);
        RuleFor(x => x.WaterEntry)
            .InclusiveBetween(1, 100);
    }

}