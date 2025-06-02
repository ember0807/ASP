// Controllers/CalculatorController.cs

using Microsoft.AspNetCore.Mvc;

namespace SimpleCalculator.Controllers
{
    public class CalculatorController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Result = null;
            ViewBag.Error = null;
            ViewBag.Number1 = null;
            ViewBag.Number2 = null;
            ViewBag.Operation = "add"; // операция по умолчанию
            return View();
        }

        [HttpPost]

        public IActionResult Calculate(double? number1, double? number2, string operation)
        {
            try
            {
                // Выводим данные для отладки через консоль
                Console.WriteLine($"number1: {number1}, number2: {number2}, operation: {operation}");

                if (number1 == null || number2 == null)
                {
                    // Если одно из чисел отсутствует
                    ViewBag.Error = "Введите оба числа.";
                    return View("Index");
                }

                double? result = null;
                string errorMessage = null;

                // Выполняем арифметическую операцию
                switch (operation?.ToLower())
                {
                    case "add":
                        result = number1 + number2;
                        break;
                    case "subtract":
                        result = number1 - number2;
                        break;
                    case "multiply":
                        result = number1 * number2;
                        break;
                    case "divide":
                        if (number2 != 0)
                        {
                            result = number1 / number2;
                        }
                        else
                        {
                            errorMessage = "Деление на ноль невозможно.";
                        }
                        break;
                    default:
                        errorMessage = "Неизвестная операция.";
                        break;
                }

                // Передача результата или ошибки в представление
                ViewBag.Result = result;
                ViewBag.Error = errorMessage ?? null;
                ViewBag.Number1 = number1;
                ViewBag.Number2 = number2;
                ViewBag.Operation = operation;

                return View("Index");
            }
            catch (Exception ex)
            {
                // Логируем ошибку для отладки
                Console.WriteLine($"Произошла ошибка: {ex.Message}");

                // Возвращаем сообщение об ошибке в интерфейс
                ViewBag.Error = "Произошла ошибка во время вычислений. Проверьте ваши данные и попробуйте снова.";
                return View("Index");
            }
        }
    }
}
