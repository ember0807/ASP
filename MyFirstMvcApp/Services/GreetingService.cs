using System;

namespace MyFirstMvcApp.Services 
{
    public interface IGreetingService
    {
        string GetWelcomeMessage();
    }

    public class GreetingService : IGreetingService
    {
        public string GetWelcomeMessage()
        {
            var currentTime = DateTime.Now;
            if (currentTime.Hour >= 5 && currentTime.Hour < 12)
            {
                return "Доброе утро!";
            }
            else if (currentTime.Hour >= 12 && currentTime.Hour < 18)
            {
                return "Добрый день!";
            }
            else if (currentTime.Hour >= 18 && currentTime.Hour < 22)
            {
                return "Добрый вечер!";
            }
            else
            {
                return "Доброй ночи!";
            }
        }
    }
}
