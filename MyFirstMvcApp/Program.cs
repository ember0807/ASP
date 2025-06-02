using MyFirstMvcApp.Services; // Добавьте using для папки Services

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы MVC
builder.Services.AddControllersWithViews();

// Регистрируем наш сервис приветствия
builder.Services.AddScoped<IGreetingService, GreetingService>(); // Или AddTransient, или AddSingleton в зависимости от нужного времени жизни

var app = builder.Build();

//Настраиваем HTTPS Redirection
app.UseHttpsRedirection();
// Включаем маршрутизацию
app.UseRouting();


// Настраиваем конечные точки (endpoints) для контроллеров
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//app.MapGet("/", () => "Hello World!");

app.Run();
