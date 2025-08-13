using Microsoft.AspNetCore.Identity;
using WellnessTracker.Data;
using WellnessTracker.Models.DTOs.CreateDTOs;
using WellnessTracker.Models.DTOs.InputDTOs;
using WellnessTracker.Services.Interfaces;
using WellnessTracker.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace WellnessTracker.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly ApplicationDBContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _configuration;
    
    public AuthService(ApplicationDBContext dbContext, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
    }
    
    public async Task RegisterAsync(UserRegisterDto userRegisterDto, CancellationToken cancellationToken)
    {
        if (userRegisterDto == null || string.IsNullOrEmpty(userRegisterDto.Email) || 
            string.IsNullOrEmpty(userRegisterDto.Password) || string.IsNullOrEmpty(userRegisterDto.FirstName) || 
            string.IsNullOrEmpty(userRegisterDto.ConfirmPassword))
            throw new ArgumentNullException(nameof(userRegisterDto), "Invalid user registration data. Not all required fields are entered.");
        
        if (userRegisterDto.Password != userRegisterDto.ConfirmPassword) 
            throw new ArgumentException("Passwords do not match.");
        
        if (userRegisterDto.Password.Length < 8)
            throw new ArgumentException("Password must be at least 8 characters long.");
        
        if (await _dbContext.Users.AnyAsync(u => u.Email == userRegisterDto.Email))
            throw new ArgumentException("Email already registered.");
        
        var user = new User
        {
            Email = userRegisterDto.Email,
            FirstName = userRegisterDto.FirstName
        };
        
        user.PasswordHash = _passwordHasher.HashPassword(user, userRegisterDto.Password);
        
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<string> LoginAsync(UserLoginDto userLoginDto)
    {
        if (userLoginDto == null || string.IsNullOrEmpty(userLoginDto.Email) || 
            string.IsNullOrEmpty(userLoginDto.Password))
            throw new ArgumentNullException(nameof(userLoginDto), "Invalid user login data. Not all required fields are entered.");
        
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userLoginDto.Email);
        if (user == null)
            throw new ArgumentException("Invalid credentials.");
        
        if (string.IsNullOrEmpty(user.PasswordHash))
            throw new InvalidOperationException("User has no password set.");
        
        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userLoginDto.Password);
        if (verificationResult != PasswordVerificationResult.Success)
            throw new ArgumentException("Invalid credentials.");
        
        var token = GenerateJwtToken(user);
        if (string.IsNullOrEmpty(token))
            throw new InvalidOperationException("Failed to generate authentication token.");
        
        return token;
    }

    public string GenerateJwtToken(User user)
    {
        if (user == null || user.ID < 1 || string.IsNullOrEmpty(user.Email))
            throw new ArgumentNullException(nameof(user), "Invalid user data.");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException());
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"])),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}