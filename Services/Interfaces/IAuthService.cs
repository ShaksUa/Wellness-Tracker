using Wellness_Tracker.Models.DTOs.CreateDTOs;
using Wellness_Tracker.Models.DTOs.InputDTOs;
using Wellness_Tracker.Models.Entities;

namespace Wellness_Tracker.Services.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(UserRegisterDto userRegisterDto);
    Task<string> LoginAsync(UserLoginDto userLoginDto);
    string GenerateJwtToken(User user);
}

