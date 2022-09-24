using Microsoft.AspNetCore.Mvc;
using NilesServer.Service;

namespace NilesServer.Controller;

[ApiController]
public class TestController {
    private readonly IDbProvider _dbProvider;

    public TestController(IDbProvider dbProvider) {
        _dbProvider = dbProvider;
    }

    [HttpGet("/api/test")]
    public string Test() {
        return _dbProvider.GetSqliteVersion() ?? string.Empty;
    }
}