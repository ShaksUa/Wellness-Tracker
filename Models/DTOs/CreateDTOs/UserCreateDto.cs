using System.ComponentModel.DataAnnotations;

namespace Wellness_Tracker.Models.DTOs.CreateDTOs;

public class UserCreateDto
{
    [Required,  StringLength(50)]
    public string FirstName { get; set; }
    [Required, StringLength(50)]
    public string LastName { get; set; }
    [Display(Name = "Date and Time of Registration")]
    public DateTime RegistrationDateTime { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Range(1, 120)]
    public int Age { get; set; }
}