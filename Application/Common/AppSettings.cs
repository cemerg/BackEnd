public class AppSettings
{
    public  required GoogleSetting Google { get; set; }
}

public class GoogleSetting
{
    public string ClientId { get; set; } = string.Empty;
}