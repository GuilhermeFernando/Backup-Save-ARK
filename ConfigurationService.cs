using Microsoft.Extensions.Configuration;

public class ConfigurationService
{
    private readonly IConfiguration _configuration;

    public ConfigurationService()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();
    }

    public BackupConfiguration GetBackupConfiguration()
    {
        return _configuration
            .GetSection("Backup")
            .Get<BackupConfiguration>() ?? throw new InvalidOperationException("A configuração de Backup não foi encontrada.");
    }
}