using FluentValidation;
using WellnessTracker.Models.DTOs.CreateDTOs;

namespace WellnessTracker.Utilities.Validation;

public class TaskValidator : AbstractValidator<TaskItemCreateDto>
{
    public TaskValidator()
    {
        RuleFor(x => x.Task)
            .MaximumLength(4000);
        RuleFor(x => x.TaskDateTime)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .When(x => !String.IsNullOrEmpty(x.Task));
        RuleFor(x => x.TaskReminderDateTime)
            .NotEmpty()
            .When(x => x.TaskReminder == true);
    }
}
