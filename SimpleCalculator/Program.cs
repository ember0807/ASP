using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы в контейнер.
builder.Services.AddControllersWithViews(); // Добавляем поддержку MVC

var app = builder.Build();

// Конфигурируем конвейер HTTP-запросов.
if (!app.Environment.IsDevelopment())
{
    // Обработчик ошибок для production-среды
    app.UseExceptionHandler("/Home/Error");
    // HSTS (HTTP Strict Transport Security).  Рекомендуется включить в production.
    app.UseHsts();
}

app.UseHttpsRedirection(); // Перенаправление на HTTPS
app.UseStaticFiles(); // Поддержка статических файлов (CSS, JS, images)

app.UseRouting(); // Включаем маршрутизацию

app.UseAuthorization(); // Включаем авторизацию (если нужна)

// Определяем маршрут по умолчанию
app.MapControllerRoute(
    name: "default", // Имя маршрута
    pattern: "{controller=Calculator}/{action=Index}/{id?}"); // Шаблон маршрута

app.Run(); // Запускаем приложение
