namespace CardActionService.Configuration.Security;

public class KeyOption
{
    public const string SectionName = "Security";
    public string ApiKey { get; set; } = string.Empty;
}
