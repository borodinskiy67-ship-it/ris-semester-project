using Microsoft.OpenApi;
using backend.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Добавляем контроллеры и Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Регистрация сервиса через DI (Dependency Injection)
// Это нужно, чтобы контроллеры не создавали сервис через new
builder.Services.AddSingleton<ITicketService, TicketService>();

var app = builder.Build();

// 3. Swagger включаем только в режиме Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

// 4. Служебные адреса (Minimal API)
app.MapGet("/health", () => Results.Ok("Healthy"));

app.MapGet("/info", (ITicketService service) => 
{
    return Results.Ok(new 
    {
        AppName = "Система учёта заявок",
        Environment = app.Environment.EnvironmentName,
        Version = "ЛР7 (Web API Framework)",
        ServiceStatus = service.GetServiceStatus()
    });
});

app.MapControllers();

app.Run();