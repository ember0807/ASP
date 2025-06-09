using ReviewApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы в контейнер.
builder.Services.AddControllersWithViews(); // Это добавляет MVC сервисы

builder.Services.AddSingleton<ReviewService>();

var app = builder.Build();

// Настраиваем конвейер HTTP-запросов.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.UseAuthorization();

app.Run();
