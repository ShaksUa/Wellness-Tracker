using System.ComponentModel.DataAnnotations;
namespace Wellness_Tracker.Models.DTOs.CreateDTOs;

public class NoteCreateDto
{
    [Display(Name = "Note Title")]
    public string? Title {get; set;}
    public string? Content {get; set;}
}