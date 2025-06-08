namespace Wellness_Tracker.Configuration;

public class LoggingConfiguration
{
    public string LogsDirectory { get; set; } = "Logs";
    public string LogFileName { get; set; } = "app.log";
    public string LogFilePath => Path.Combine(LogsDirectory, LogFileName);
}