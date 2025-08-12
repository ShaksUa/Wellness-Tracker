using System.ComponentModel.DataAnnotations;

namespace WellnessTracker.Models.DTOs.CreateDTOs;

public class UserRegisterDto
{
    [StringLength(50)]
    public required string FirstName { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    [StringLength(50)]
    public string? LastName { get; set; }
    [Range(1, 120)]
    public int? Age { get; set; }
    [StringLength(100, MinimumLength = 8)]
    public required string Password { get; set; }
    [StringLength(100, MinimumLength = 8)]
    public required string ConfirmPassword { get; set; }

}