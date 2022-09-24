using Microsoft.Data.Sqlite;

namespace NilesServer.Service;

public interface IDbProvider {
    SqliteConnection GetConnection();

    string? GetSqliteVersion();
}