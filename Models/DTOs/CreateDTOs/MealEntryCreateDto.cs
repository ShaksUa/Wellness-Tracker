using System.ComponentModel.DataAnnotations;

namespace Wellness_Tracker.Models.DTOs.CreateDTOs;

public class MealEntryCreateDto
{
    public string? Breakfast { get; set; }
    [Display(Name = "Date and Time of Breakfast"), DataType(DataType.DateTime)]
    public DateTime BreakfastDateTime { get; set; }
    public string? Lunch { get; set; }
    [Display(Name = "Date and Time of Lunch"),DataType(DataType.DateTime)]
    public DateTime LunchDateTime { get; set; }
    public string Dinner { get; set; }
    [Display(Name = "Date and Time of Dinner"),DataType(DataType.DateTime)]
    public DateTime DinnerDateTime { get; set; }
    public string Supper { get; set; }
    [Display(Name = "Date and Time of Supper"),DataType(DataType.DateTime)]
    public DateTime SupperDateTime { get; set; }
    public string? OtherMealEntry { get; set; }
    [Display(Name = "Date and Time of Other Meal Entry"),DataType(DataType.DateTime)]
    public DateTime OtherMealEntryDateTime { get; set; }
    [Range(1,100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public string? WaterEntry { get; set; }
}