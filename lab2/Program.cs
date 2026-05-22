using E2EEMessenger.Data;
using E2EEMessenger.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Підключаємо SQLite базу даних
builder.Services.AddDbContext<MessengerContext>(options =>
    options.UseSqlite("Data Source=messenger.db"));

// Підключаємо нашу бізнес-логіку
builder.Services.AddScoped<IMessageService, MessageService>();

var app = builder.Build();

// Створюємо базу даних при запуску, якщо її немає
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MessengerContext>();
    db.Database.EnsureCreated();
}

app.UseAuthorization();
app.MapControllers();

app.Run();

// Необхідно для роботи інтеграційних тестів
public partial class Program { }