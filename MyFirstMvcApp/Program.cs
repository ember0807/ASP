using MyFirstMvcApp.Services; // �������� using ��� ����� Services

var builder = WebApplication.CreateBuilder(args);

// ��������� ������� MVC
builder.Services.AddControllersWithViews();

// ������������ ��� ������ �����������
builder.Services.AddScoped<IGreetingService, GreetingService>(); // ��� AddTransient, ��� AddSingleton � ����������� �� ������� ������� �����

var app = builder.Build();

//����������� HTTPS Redirection
app.UseHttpsRedirection();
// �������� �������������
app.UseRouting();


// ����������� �������� ����� (endpoints) ��� ������������
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//app.MapGet("/", () => "Hello World!");

app.Run();
