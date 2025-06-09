namespace Wellness_Tracker.Models.DTOs.CreateDTOs;

public class UserCreateDto
{
    public required string  LastName { get; set; }
    public string? FirstName { get; set; }
    public DateTime RegistrationDateTime { get; set; }
}