namespace Wellness_Tracker.Models.DTOs.ReadDTOs;

public class UserReadDto
{
    public int ID { get; set; }
    public required string  LastName { get; set; }
    public string? FirstName { get; set; }
    public DateTime RegistrationDateTime { get; set; }
}