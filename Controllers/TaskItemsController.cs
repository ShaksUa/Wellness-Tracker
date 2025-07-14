using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WellnessTracker.Data;
using WellnessTracker.Models.DTOs.CreateDTOs;
using WellnessTracker.Models.DTOs.ReadDTOs;
using WellnessTracker.Models.Entities;
namespace WellnessTracker.Controllers;

[ApiController]
[Route("[controller]")]

public class TaskItemsController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    
    public TaskItemsController(ApplicationDBContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create (TaskItemCreateDto taskItem)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var newTask = new TaskItem
        {
            Task = taskItem.Task,
            TaskDateTime = taskItem.TaskDateTime,
            TaskReminder = taskItem.TaskReminder,
            TaskReminderDateTime = taskItem.TaskReminderDateTime
        };
        
        _context.TaskItems.Add(newTask);
        await _context.SaveChangesAsync();
        return Ok(new {id = newTask.ID});
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItemReadDto>>> GetAll()
    {
       // var taskItems = _context.TaskItems.ToList();

       var taskItems = await _context.TaskItems
           .Select(e => new TaskItemReadDto
           {
               ID = e.ID,
               Task = e.Task,
               TaskDateTime = e.TaskDateTime,
               TaskReminder = e.TaskReminder,
               TaskReminderDateTime = e.TaskReminderDateTime
           })
           .ToListAsync();
        
        if (!taskItems.Any())
        {
            return NotFound("No task items found");
        }
        return Ok(taskItems);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetById(int id)
    {
        if (id < 1)
            return BadRequest("Invalid entry ID");
        
        var taskItem = await _context.TaskItems.FindAsync(id);
        if (taskItem == null)
        {
            return NotFound();
        }
        return Ok(taskItem);
    }
}

