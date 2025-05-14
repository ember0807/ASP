using ASP.lesson1.Pages;
using Autofac; // Подключение пространства имен Autofac
using Autofac.Extensions.DependencyInjection; // Подключение пространства имен для интеграции с ASP.NET Core

namespace ASP.lesson1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Создание builder для приложения
            var builder = WebApplication.CreateBuilder(args);

            // Регистрация Autofac как DI контейнера
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            // Регистрация служб ASP.NET Core
            builder.Services.AddRazorPages();

            // Создание контейнера Autofac
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterType<RestaurantMenuService>().AsSelf().InstancePerLifetimeScope(); // Регистрация сервиса с Autofac
            });

            var app = builder.Build();

            // Настройка конвейера обработки HTTP запросов
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
                context.Response.Redirect("/Home_pages"); // путь на случай отсутствия индекс файла
                return Task.CompletedTask;
            });

            app.Run();
        }
    }
}
