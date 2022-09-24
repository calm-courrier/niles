namespace NilesServer.Service.Implementation;

public class ConfigurationService : IConfigurationService {
    private readonly IConfiguration _configuration;

    public ConfigurationService(IConfiguration configuration) {
        _configuration = configuration;
    }

    public string GetDbPath() {
        return _configuration["DatabasePath"] ?? "./db.sqlite";
    }
}