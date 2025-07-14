using System.ComponentModel.DataAnnotations;

namespace WellnessTracker.Models.DTOs.CreateDTOs;

public class TaskItemCreateDto
{
    public string? Task { get; set; }
    [Display(Name = "Date and Time")]
    public DateTime TaskDateTime { get; set; }
    public bool? TaskReminder { get; set; }
    [Display(Name = "Date and Time Reminder")]
    public DateTime TaskReminderDateTime { get; set; }
}