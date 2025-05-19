using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace HomeWork6_7.Areas.Admin.Pages
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserModel : PageModel
    {
        // Список пользователей
        private static readonly List<User> Users = new List<User>
        {
            new User { Id = 1, Name = "перс 1" },
            new User { Id = 2, Name = "перс 2" },
            new User { Id = 3, Name = "перс 3" }
        };

        public User CurrentUser { get; private set; }

        public void OnGet(int id)
        {
            // Находим пользователя по ID
            CurrentUser = Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
