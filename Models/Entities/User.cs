namespace WellnessTracker.Models.Entities;

public class User
{
        public int ID { get; set; }
        public required string FirstName { get; set; }
        public string?  LastName { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public string? Email { get; set; }
        public int? Age { get; set; }
        public string? PasswordHash { get; set; }
}
