using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// ��������� ������� � ���������.
builder.Services.AddControllersWithViews(); // ��������� ��������� MVC

var app = builder.Build();

// ������������� �������� HTTP-��������.
if (!app.Environment.IsDevelopment())
{
    // ���������� ������ ��� production-�����
    app.UseExceptionHandler("/Home/Error");
    // HSTS (HTTP Strict Transport Security).  ������������� �������� � production.
    app.UseHsts();
}

app.UseHttpsRedirection(); // ��������������� �� HTTPS
app.UseStaticFiles(); // ��������� ����������� ������ (CSS, JS, images)

app.UseRouting(); // �������� �������������

app.UseAuthorization(); // �������� ����������� (���� �����)

// ���������� ������� �� ���������
app.MapControllerRoute(
    name: "default", // ��� ��������
    pattern: "{controller=Calculator}/{action=Index}/{id?}"); // ������ ��������

app.Run(); // ��������� ����������
