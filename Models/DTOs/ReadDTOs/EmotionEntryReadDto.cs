namespace WellnessTracker.Models.DTOs.ReadDTOs;

public class EmotionEntryReadDto
    {
        public int ID { get; set; }
        public int GeneralMood { get; set; }
        public DateTime GeneralMoodDateTime { get; set; }
        public string? Wins { get; set; }
        public string? Losses { get; set; }
        public string? Comments { get; set; }

}