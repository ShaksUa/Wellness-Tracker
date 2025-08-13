using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WellnessTracker.Models.DTOs.CreateDTOs;
using WellnessTracker.Models.DTOs.InputDTOs;
using WellnessTracker.Services.Interfaces;

namespace WellnessTracker.Controllers;
[Authorize] 
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }
    
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto, CancellationToken cancellationToken)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _authService.RegisterAsync(registerDto, cancellationToken);
            
            return CreatedAtAction(nameof(Register), new { email = registerDto.Email });
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogWarning("Registration failed due to invalid input: {Message}", ex.Message);
            return BadRequest(new { message = "Invalid registration data provided." });
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Registration failed: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during user registration");
            return StatusCode(500, new { message = "An error occurred during registration. Please try again later." });
        }
    }
    
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] UserLoginDto loginDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _authService.LoginAsync(loginDto);
            
            return Ok(new AuthResponseDto 
            { 
                Token = token,
                Email = loginDto.Email
            });
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogWarning("Login failed due to invalid input: {Message}", ex.Message);
            return BadRequest(new { message = "Invalid login data provided." });
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Login failed: {Message}", ex.Message);
            return Unauthorized(new { message = "Invalid credentials." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during user login");
            return StatusCode(500, new { message = "An error occurred during login. Please try again later." });
        }
    }
}

