using Microsoft.Data.Sqlite;

namespace NilesServer.Service.Implementation;

public class DbProvider : IDbProvider {
    private readonly IConfigurationService _configurationService;

    public DbProvider(IConfigurationService configurationService) {
        _configurationService = configurationService;
    }

    public SqliteConnection GetConnection() {
        return new SqliteConnection("Data Source=" + _configurationService.GetDbPath());
    }

    public string? GetSqliteVersion() {
        using var con = GetConnection();
        con.Open();
        var command = con.CreateCommand();
        command.CommandText = "select sqlite_version();";

        using var reader = command.ExecuteReader();
        if (!reader.Read()) return null;
        var version = reader.GetString(0);
        return version;
    }
}