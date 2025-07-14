using System.ComponentModel.DataAnnotations;
namespace WellnessTracker.Models.DTOs.CreateDTOs;

public class NoteCreateDto
{
    [Display(Name = "Note Title")]
    public string? Title {get; set;}
    public string? Content {get; set;}
}