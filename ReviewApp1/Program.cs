using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ReviewApp.Services;

var builder = WebApplication.CreateBuilder(args);

// ��������� ������� MVC
builder.Services.AddControllersWithViews();

// ��������� ������ �������
builder.Services.AddSingleton<IReviewService, ReviewService>();

var app = builder.Build();

// ����������� �������������
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseStaticFiles();

app.Run();
