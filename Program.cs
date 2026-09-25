var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/health", () =>
{
    return Results.Ok(new
    {
        status = "Healthy",
        application = "Employee Management System",
        framework = ".NET 11",
        timestamp = DateTime.UtcNow
    });
});

// your other API endpoints here

app.Run();
