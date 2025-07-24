using Serilog;
using Serilog.Events;

namespace CardActionService.Configuration.Logging;

public static class LoggingSetup
{
    public static void ConfigureLogger()
    {
        var logFilePath = LogPathResolver.GetLogFilePath();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(
                path: logFilePath,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7,
                shared: true)
            .CreateLogger();
    }
}