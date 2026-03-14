using ApplicationTracker.Data;
using ApplicationTracker.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------------------------------------
// Services
// ------------------------------------------------------------------

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        // Serialize enums as their string names instead of integers.
        opts.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// SQLite — database file is created next to the binary
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("Default")
        ?? "Data Source=job_applications.db"));

builder.Services.AddScoped<IJobApplicationService, JobApplicationService>();

// ------------------------------------------------------------------
// Pipeline
// ------------------------------------------------------------------

var app = builder.Build();

// Apply any pending EF migrations automatically on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();   // Creates the schema if it doesn't exist yet.
                                   // Swap to db.Database.Migrate() once you add
                                   // proper EF migrations via `dotnet ef migrations add`.
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // OpenAPI JSON available at /openapi/v1.json
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();