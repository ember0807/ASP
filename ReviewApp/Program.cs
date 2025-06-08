using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ReviewApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы MVC
builder.Services.AddControllersWithViews();

// Добавляем сервис отзывов
builder.Services.AddSingleton<IReviewService, ReviewService>();

var app = builder.Build();

// Настраиваем маршрутизацию
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseStaticFiles();

app.Run();
