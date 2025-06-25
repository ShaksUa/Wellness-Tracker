using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wellness_Tracker.Data;
using Wellness_Tracker.Models.DTOs.CreateDTOs;
using Wellness_Tracker.Models.DTOs.ReadDTOs;
using Wellness_Tracker.Models.Entities;

namespace Wellness_Tracker.Controllers;

[ApiController]
[Route("[controller]")]

public class MealEntriesController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    public MealEntriesController(ApplicationDBContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<MealEntry>> Create(MealEntryCreateDto mealEntry)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var newMeal = new MealEntry
        {
            Breakfast = mealEntry.Breakfast,
            BreakfastDateTime = mealEntry.BreakfastDateTime,
            Lunch = mealEntry.Lunch,
            LunchDateTime = mealEntry.LunchDateTime,
            Dinner = mealEntry.Dinner,
            DinnerDateTime = mealEntry.DinnerDateTime,
            Supper = mealEntry.Supper,
            SupperDateTime = mealEntry.SupperDateTime,
            OtherMealEntry = mealEntry.OtherMealEntry,
            OtherMealEntryDateTime = mealEntry.OtherMealEntryDateTime,
            WaterEntry = mealEntry.WaterEntry
        };

        try
        {
            _context.Meals.Add(newMeal);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Create), new { id = newMeal.ID }, newMeal);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while creating the meal entry");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MealEntryReadDto>>> GetAll()
    {
        var mealEntry = await _context.Meals
            .Select(e => new MealEntryReadDto
            {
                ID = e.ID,
                Breakfast = e.Breakfast,
                BreakfastDateTime = e.BreakfastDateTime,
                Lunch = e.Lunch,
                LunchDateTime = e.LunchDateTime,
                Dinner = e.Dinner,
                DinnerDateTime = e.DinnerDateTime,
                Supper = e.Supper,
                SupperDateTime = e.SupperDateTime,
            })
            .ToListAsync();

        if (!mealEntry.Any())
        {
            return NotFound("No meal entries found");
        }
        return Ok(mealEntry);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<MealEntry>> GetById(int id)
    {
        if (id < 1)
            return BadRequest("Invalid entry ID");
        
        var mealEntry = await _context.Meals.FindAsync(id);
        if (mealEntry == null)
        {
            return NotFound();
        }
        return Ok(mealEntry);
    }
}