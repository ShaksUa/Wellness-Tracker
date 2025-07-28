using System.ComponentModel.DataAnnotations;

namespace WellnessTracker.Models.DTOs.UpdateDTOs;

public class EmotionEntryUpdateDto
{
    public int ID { get; set; }
    [Required,Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int GeneralMood { get; set; } 
    [Display(Name = "Date and Time")] 
    public DateTime GeneralMoodDateTime { get; set; }
    public string? Wins { get; set; }
    public string? Losses { get; set; }
    public string? Comments { get; set; }

}