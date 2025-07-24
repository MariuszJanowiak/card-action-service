using System.Runtime.InteropServices;

namespace CardActionService.Configuration.Logging;

public static class LogPathResolver
{
    public static string GetLogFilePath()
    {
        string logFolder;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // AppData\Roaming\CardActionService\logs
            logFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "CardActionService",
                "logs");
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            // ~/Library/Logs/CardActionService
            logFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "Library",
                "Logs",
                "CardActionService");
        }
        else
        {
            // ~/.local/share/cardactionservice/logs
            logFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "cardactionservice",
                "logs");
        }

        Directory.CreateDirectory(logFolder);

        return Path.Combine(logFolder, "service.log");
    }
}