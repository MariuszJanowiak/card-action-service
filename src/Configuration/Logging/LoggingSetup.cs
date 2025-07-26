using Serilog;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace CardActionService.Configuration.Logging;

public static class LoggingSetup
{
    public static ILogger ConfigureLogger()
    {
        var logFilePath = LogPathResolver.GetLogFilePath();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(
                logFilePath,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7,
                shared: true)
            .CreateLogger();

        return Log.Logger;
    }
}