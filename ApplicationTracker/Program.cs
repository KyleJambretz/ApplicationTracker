var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
// We can keep this in development mode for now before any builds are made
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi().RequireAuthorization("ApiTesterPolicy");
    app.MapGet("/", () => "");
}

app.UseAuthorization();

app.MapControllers();

app.Run();
