using Microsoft.AspNetCore.Mvc;
using Serilog;
using WellnessTracker.Data;
using WellnessTracker.Models.DTOs.ReadDTOs;
using WellnessTracker.Models.DTOs.CreateDTOs;
using WellnessTracker.Models.Entities;

namespace WellnessTracker.Controllers;
[ApiController]
[Route("[controller]")]
public class EmotionEntriesController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    
    public EmotionEntriesController(ApplicationDBContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<EmotionEntry>> Create(EmotionEntryCreateDto entry)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var newEntry = new EmotionEntry
        {
            GeneralMood = entry.GeneralMood,
            GeneralMoodDateTime = entry.GeneralMoodDateTime,
            Wins = entry.Wins,
            Losses = entry.Losses,
            Comments = entry.Comments
        };
        _context.EmotionEntries.Add(newEntry);
        await _context.SaveChangesAsync();
        return Ok(new {id = newEntry.ID});
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<EmotionEntryReadDto>> GetAll()
    {
        try
        {
            var entries = _context.EmotionEntries
                .Select(e => new EmotionEntryReadDto
                {
                    ID = e.ID,
                    GeneralMood = e.GeneralMood,
                    GeneralMoodDateTime = e.GeneralMoodDateTime,
                    Comments = e.Comments
                }).ToList();
        
        
            if (!entries.Any())
            {
                return NotFound("No entries found");
            }
        
            return Ok(entries);
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "Error retrieving entries");
            return StatusCode(500, "Error retrieving entries");
        }
        
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmotionEntry>> GetById(int id)
    {   
        if (id < 1)
            return BadRequest("Invalid entry ID");
        
        var entry = await _context.EmotionEntries.FindAsync(id);
        if (entry == null)
        {
            return NotFound();
        }
        return Ok(entry);
    }
}