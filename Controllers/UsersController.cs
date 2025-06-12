using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wellness_Tracker.Data;
using Wellness_Tracker.Models.DTOs.CreateDTOs;
using Wellness_Tracker.Models.DTOs.ReadDTOs;
using Wellness_Tracker.Models.Entities;

[ApiController]
[Route("[controller]")]

public class UsersController: ControllerBase
{
    private readonly ApplicationDBContext _context;
    public UsersController(ApplicationDBContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<User>> Create(UserCreateDto user)
    {
        var newUser = new User
        {
            LastName = user.LastName,
            FirstName = user.FirstName,
            RegistrationDateTime = user.RegistrationDateTime
        };
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        return Ok(new {id = newUser.ID});
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
                RegistrationDateTime = e.RegistrationDateTime
            })
            .ToListAsync();
        
        if (!users.Any())
        {
            return NotFound("No users found");
        }
        return Ok(users);
    }
}