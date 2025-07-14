using System.ComponentModel.DataAnnotations;

namespace WellnessTracker.Models.DTOs.CreateDTOs;

public class UserCreateDto
{
    [Required,  StringLength(50)]
    public string FirstName { get; set; }
    [StringLength(50)]
    public string? LastName { get; set; }
    [Display(Name = "Date and Time of Registration")]
    public DateTime RegistrationDateTime { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [Range(1, 120)]
    public int? Age { get; set; }
    [Required, StringLength(100, MinimumLength = 8)]
    public string Password { get; set; }
}