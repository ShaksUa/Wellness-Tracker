namespace WellnessTracker.Models.Entities;

public class TaskItem
{
    public int ID { get; set; }
    public string? Task { get; set; }
    public DateTime TaskDateTime { get; set; }
    public bool? TaskReminder { get; set; }
    public DateTime TaskReminderDateTime { get; set; }
}