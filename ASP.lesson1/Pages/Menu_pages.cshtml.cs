using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ASP.lesson1.Pages
{
	public class MenuItem
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
	}

	public class RestaurantMenuService
	{
		public List<MenuItem> GetMenuItems()
		{
			return new List<MenuItem>
			{
				new MenuItem { Id = 1, Name = "Sushi", Price = 500 },
				new MenuItem { Id = 2, Name = "Ramen", Price = 450 },
				new MenuItem { Id = 3, Name = "Tempura", Price = 600 },
				new MenuItem { Id = 4, Name = "Takoyaki", Price = 400 },
				new MenuItem { Id = 5, Name = "Miso Soup", Price = 250 },
			};
		}
	}

	public class Index1Model : PageModel
	{
		private readonly RestaurantMenuService _restaurantMenuService;

		public Index1Model(RestaurantMenuService restaurantMenuService)
		{
			_restaurantMenuService = restaurantMenuService;
		}

		// Добавлено свойство для хранения элементов меню
		public List<MenuItem> MenuItems { get; private set; }

		public void OnGet()
		{
			MenuItems = _restaurantMenuService.GetMenuItems();
		}
	}
}
