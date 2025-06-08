using Microsoft.AspNetCore.Mvc;
using Wellness_Tracker.Data;

[ApiController]
[Route("api/[controller]")]
public class EmotionEntriesController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    
    public EmotionEntriesController(ApplicationDBContext context)
    {
        _context = context;
    }
}