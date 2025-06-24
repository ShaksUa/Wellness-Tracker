using System.ComponentModel.DataAnnotations;

namespace Wellness_Tracker.Models.DTOs.CreateDTOs;

public class EmotionEntryCreateDto
{
        [Required,Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public string GeneralMood { get; set; } = string.Empty; 
        [Display(Name = "Date and Time")] 
        public DateTime GeneralMoodDateTime { get; set; }
        public string? Wins { get; set; }
        public string? Losses { get; set; }
        public string? Comments { get; set; }

}