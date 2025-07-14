using Microsoft.AspNetCore.Mvc;
using WellnessTracker.Models.Entities;

namespace WellnessTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthCheckController(ILogger<HealthCheckController> logger) : ControllerBase
{
    private static readonly string[] Summaries = ["Ok","Nok","NA"];

    [HttpGet(Name = "GetHealthCheck")]
    public IEnumerable<HealthCheck> Get()
    {
        logger.LogInformation("Getting health check status");
        
        var healthCheck = Enumerable.Range(1, 3).Select(index => new HealthCheck {
        //return Enumerable.Range(1, 3).Select(index => new HealthCheck {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        logger.LogInformation("Retrieved Count: {Count} from health check entries " , healthCheck.Length);
       
        return healthCheck; 
    }
    
    [HttpGet("status")]
    public string GetHealthStatus(HealthCheck check) => check.Summary switch
    {
        "Ok" => "System is healthy",
        "Nok" => "System needs attention",
        _ => "Status unknown"
    };
    
}