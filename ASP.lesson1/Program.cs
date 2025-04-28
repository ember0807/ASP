using ASP.lesson1.Pages;

namespace ASP.lesson1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// ����������� �����
			builder.Services.AddRazorPages();
			builder.Services.AddScoped<RestaurantMenuService>(); // ����������� �������

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
