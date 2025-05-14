using ASP.lesson1.Pages;
using Autofac; // ����������� ������������ ���� Autofac
using Autofac.Extensions.DependencyInjection; // ����������� ������������ ���� ��� ���������� � ASP.NET Core

namespace ASP.lesson1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // �������� builder ��� ����������
            var builder = WebApplication.CreateBuilder(args);

            // ����������� Autofac ��� DI ����������
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            // ����������� ����� ASP.NET Core
            builder.Services.AddRazorPages();

            // �������� ���������� Autofac
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterType<RestaurantMenuService>().AsSelf().InstancePerLifetimeScope(); // ����������� ������� � Autofac
            });

            var app = builder.Build();

            // ��������� ��������� ��������� HTTP ��������
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapGet("/", context =>
            {
                context.Response.Redirect("/Home_pages"); // ���� �� ������ ���������� ������ �����
                return Task.CompletedTask;
            });

            app.Run();
        }
    }
}
