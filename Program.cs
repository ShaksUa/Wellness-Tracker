using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Configure logging
builder.Logging.ClearProviders();
//builder.Logging.AddConsole(); 
//builder.Logging.AddEventLog();  //Windows Event Log 
try
{
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .WriteTo.Console()
        .WriteTo.File("Logs/app_.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();
    builder.Host.UseSerilog();
}
catch (Exception ex)
{
    Console.WriteLine($"Error configuring logging: {ex.Message}");
    throw;
}

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//### Swagger Configuration
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Wellness Tracker API",
        Version = "v1",
        Description = "API for the Wellness Tracker application."
    });
});

//### CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .WithMethods("GET","POST","PUT","DELETE","OPTIONS");
        });
});

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80); // Listen to port 80 inside the container
});


var app = builder.Build();

// ### Development Environment Configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Wellness Tracker API v1");
        options.RoutePrefix = string.Empty;
    });
}

//### Middleware Pipeline
app.UseRouting();
app.UseCors(myAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();