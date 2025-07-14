using WellnessTracker.Models.Entities;

namespace WellnessTracker.Services.Interfaces;

public interface IUserRepository
{
    User? GetById(int id);
}

