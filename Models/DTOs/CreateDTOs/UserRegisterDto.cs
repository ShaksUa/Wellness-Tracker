using System.ComponentModel.DataAnnotations;

namespace WellnessTracker.Models.DTOs.CreateDTOs;

public class UserRegisterDto
{
    [Required, StringLength(50)]
    public string FirstName { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [StringLength(50)]
    public string? LastName { get; set; }
    [Range(1, 120)]
    public int? Age { get; set; }
    [Required, StringLength(100, MinimumLength = 8)]
    public string Password { get; set; }
    [Required, StringLength(100, MinimumLength = 8)]
    public string ConfirmPassword { get; set; }

}