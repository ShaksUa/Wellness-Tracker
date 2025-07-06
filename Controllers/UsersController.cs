using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wellness_Tracker.Data;
using Wellness_Tracker.Models.DTOs.CreateDTOs;
using Wellness_Tracker.Models.DTOs.ReadDTOs;
using Wellness_Tracker.Models.Entities;
namespace Wellness_Tracker.Controllers;
[ApiController]
[Route("[controller]")]

public class UsersController: ControllerBase
{
    private readonly ApplicationDBContext _context;
    public UsersController(ApplicationDBContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAll()
    {
        var users = await _context.Users
            .Select(e => new UserReadDto
            {
                ID = e.ID,
                LastName = e.LastName,
                FirstName = e.FirstName,
                RegistrationDateTime = e.RegistrationDateTime,
                Email = e.Email,
                Age = e.Age,
            })
            .ToListAsync();
        
        if (!users.Any())
        {
            return NotFound("No users found");
        }
        return Ok(users);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(int id)
    {
        if (id < 1)
            return BadRequest("Invalid entry ID");
        
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
}