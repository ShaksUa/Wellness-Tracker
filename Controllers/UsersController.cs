using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WellnessTracker.Data;
using WellnessTracker.Models.DTOs.CreateDTOs;
using WellnessTracker.Models.DTOs.ReadDTOs;
using WellnessTracker.Models.Entities;
namespace WellnessTracker.Controllers;

[Authorize] 
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
    public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAll(CancellationToken cancellationToken)
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
            .ToListAsync(cancellationToken);
        
        if (!users.Any())
        {
            return NotFound("No users found");
        }
        return Ok(users);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(int id,CancellationToken cancellationToken)
    {
        if (id < 1)
            return BadRequest("Invalid entry ID");
        
        var user = await _context.Users.FindAsync(id,cancellationToken);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
}