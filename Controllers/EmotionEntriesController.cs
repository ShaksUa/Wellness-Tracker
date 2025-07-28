using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WellnessTracker.Data;
using WellnessTracker.Models.DTOs.ReadDTOs;
using WellnessTracker.Models.DTOs.CreateDTOs;
using WellnessTracker.Models.DTOs.UpdateDTOs;
using WellnessTracker.Models.Entities;

namespace WellnessTracker.Controllers;

[Authorize] 
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
            return NotFound("The requested entry was not found.");
        }
        return Ok(entry);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<EmotionEntry>> DeleteById(int id)
    {
        if (id < 1)
        {
            return BadRequest("Invalid entry ID");
        } 
        try
        {
            var emotionEntry = await _context.EmotionEntries.FirstAsync(e => e.ID == id);
            _context.EmotionEntries.Remove(emotionEntry);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (InvalidOperationException)
        {
           return NotFound($"Emotion entry with ID {id} was not found.");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error removing emotion entry with ID {EmotionEntryId}", id);
            return StatusCode(500, "An error occurred while removing the emotion entry.");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EmotionEntry>> UpdateById(int id, EmotionEntryUpdateDto updateDto)
    {
         if (id != updateDto.ID)
         {
             return BadRequest("Invalid entry ID");
         }
         try
         {
             var entry = await _context.EmotionEntries.FirstAsync(e => e.ID == id);
             entry.GeneralMood = updateDto.GeneralMood;
             entry.GeneralMoodDateTime = updateDto.GeneralMoodDateTime;
             entry.Wins = updateDto.Wins;
             entry.Losses = updateDto.Losses;
             entry.Comments = updateDto.Comments;
             
             _context.EmotionEntries.Update(entry);
             await _context.SaveChangesAsync();
             return Ok(entry);
         }
         catch (InvalidOperationException)
         {
             return NotFound($"Emotion entry with ID {id} was not found.");
         }
         catch (Exception e)
         {
             Log.Error(e, "Error updating emotion entry with ID {EmotionEntryId}", id);
             return StatusCode(500, "An error occurred while updating the emotion entry.");
         }
    }
}