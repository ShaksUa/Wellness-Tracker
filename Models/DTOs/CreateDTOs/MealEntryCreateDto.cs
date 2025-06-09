namespace Wellness_Tracker.Models.DTOs.CreateDTOs;

public class MealEntryCreateDto
{
    public string? Breakfast { get; set; }
    public DateTime BreakfastDateTime { get; set; }
    public string? Lunch { get; set; }
    public DateTime LunchDateTime { get; set; }
    public string? Dinner { get; set; }
    public DateTime DinnerDateTime { get; set; }
    public string? Supper { get; set; }
    public DateTime SupperDateTime { get; set; }
    public string? OtherMealEntry { get; set; }
    public DateTime OtherMealEntryDateTime { get; set; }
    public string? WaterEntry { get; set; }
}