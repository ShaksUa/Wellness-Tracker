using WellnessTracker.Models.Entities;
using WellnessTracker.Services.Interfaces;

namespace WellnessTracker.Services.Implementations;

public class UserService : IUserRepository
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public User? GetById(int id)
    {
        return _userRepository.GetById(id);
    }
}