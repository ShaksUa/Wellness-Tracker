namespace Wellness_Tracker.Models.DTOs;

public class EmotionEntryReadDto
    {
        public int ID { get; set; }
        public string GeneralMood { get; set; } = string.Empty;
        public DateTime GeneralMoodDateTime { get; set; }
        public string? Wins { get; set; }
        public string? Losses { get; set; }
        public string? Comments { get; set; }

}