using Microsoft.Extensions.FileProviders;
using NilesServer.Service;
using NilesServer.Service.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();
builder.Services.AddSingleton<IDbProvider, DbProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Redirect to SPA if path does not start with /api and does not have extension (static file)
app.Use(async (context, next) =>
{
    await next();
    var path = context.Request.Path.Value;

    if (path != null && !path.StartsWith("/api") && !Path.HasExtension(path)) {
        context.Request.Path = "/index.html";
        await next();
    }
});

app.MapControllers();

// Don't host SPA static files for DEV - use "npm run dev" on client instead
if (!app.Environment.IsDevelopment()) {
    app.UseStaticFiles(new StaticFileOptions
        { FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "app")) });
}

app.Run();