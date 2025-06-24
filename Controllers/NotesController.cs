using Microsoft.AspNetCore.Mvc;
using Wellness_Tracker.Data;
using Wellness_Tracker.Models.DTOs.CreateDTOs;
using Wellness_Tracker.Models.DTOs.ReadDTOs;
using Wellness_Tracker.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Wellness_Tracker.Controller;

[ApiController]
[Route("[controller]")]

public class NotesController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    public NotesController(ApplicationDBContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Note>> Create(NoteCreateDto note)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var newNote = new Note
        {
            Title = note.Title,
            Content = note.Content,
        };
        _context.Notes.Add(newNote);
        await _context.SaveChangesAsync();
        return Ok(new {id = newNote.ID});
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NoteReadDto>>> GetAll()
    {
        try
        {
            var notes = await _context.Notes
                .Select(e => new NoteReadDto
                {
                    ID = e.ID,
                    Title = e.Title,
                    Content = e.Content
                })
                .ToListAsync();

            if (!notes.Any())
            {
                return NotFound("No notes found");
            }

            return Ok(notes);
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "Error retrieving notes");
            return StatusCode(500, "Error retrieving notes");
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Note>> GetById(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null)
        {
            return NotFound();
        }
        return Ok(note);
    }
    
}