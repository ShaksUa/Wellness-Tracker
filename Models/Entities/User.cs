namespace Wellness_Tracker.Models.Entities;

public class User
{
        public int ID { get; set; }
        public required string  LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public string? Email { get; set; }
        public int Age { get; set; }
}
