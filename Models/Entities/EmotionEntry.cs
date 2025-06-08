namespace Wellness_Tracker.Models.Entities;

public class EmotionEntry
{
    public int ID { get; set; }
    public required string GeneralMood { get; set; }
    public DateTime GeneralMoodDateTime { get; set; }
    public string? Wins { get; set; }
    public string? Losses { get; set; }
    public string? Comments { get; set; }
}