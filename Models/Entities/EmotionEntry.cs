namespace WellnessTracker.Models.Entities;

public class EmotionEntry
{
    public int ID { get; set; }
    public required int GeneralMood { get; set; }
    public DateTime GeneralMoodDateTime { get; set; }
    public string? Wins { get; set; }
    public string? Losses { get; set; }
    public string? Comments { get; set; }
}