namespace WellnessTracker.Models.Entities;
public class HealthCheck
{
    public int ID { get; set; }
    public DateOnly Date { get; set; }
    public string? Summary { get; set; }
}