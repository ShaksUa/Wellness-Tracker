namespace Wellness_Tracker.Models.DTOs;

public class TaskItemCreateDto
{
    public string? Task { get; set; }
    public DateTime TaskDateTime { get; set; }
    public bool? TaskReminder { get; set; }
    public DateTime TaskReminderDateTime { get; set; }
}