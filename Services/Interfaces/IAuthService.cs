using WellnessTracker.Models.DTOs.CreateDTOs;
using WellnessTracker.Models.DTOs.InputDTOs;
using WellnessTracker.Models.Entities;

namespace WellnessTracker.Services.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(UserRegisterDto userRegisterDto, CancellationToken cancellationToken);
    Task<string> LoginAsync(UserLoginDto userLoginDto);
    string GenerateJwtToken(User user);
}

