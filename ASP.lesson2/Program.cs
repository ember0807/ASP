using Microsoft.Extensions.DependencyInjection;

namespace ASP.lesson2
{
	public interface ILogger
	{
		void Log(string message);
	}
	public class ConsoleLogger : ILogger
	{
		public void Log(string message)
		{
			Console.WriteLine(message);
		}
	}
	public interface IMusic
	{
		string GetGenre();
	}
	public class ClassicalMusic : IMusic
	{
		public string GetGenre()
		{
			return "Classical";
		}
	}
	public class MusicPlayer
	{
		private readonly ILogger _logger;
		private readonly IMusic _musicGenre;

		public MusicPlayer(ILogger logger, IMusic musicGenre)
		{
			_logger = logger;
			_musicGenre = musicGenre;
		}

		public void PlayMusic()
		{
			string genre = _musicGenre.GetGenre();
			_logger.Log($"Playing {genre} music...");
		}
	}


class Program
	{
		static void Main(string[] args)
		{
			//Dependency Injection(DI) контейнер — это инструмент, используемый для управления зависимостями объектов в приложении.
			//Он помогает автоматизировать процесс создания объектов и управления их жизненным циклом,
			//что в свою очередь улучшает структуры кода и делает его более гибким и масштабируемым.
			//класс MusicPlayer использует ILogger для логирования логер зависимость от плеера
			// Создание DI-контейнера
			var serviceCollection = new ServiceCollection();

			// Регистрация всех интерфейсов и классов
			serviceCollection.AddSingleton<ILogger, ConsoleLogger>();
			serviceCollection.AddSingleton<IMusic, ClassicalMusic>();
			serviceCollection.AddSingleton<MusicPlayer>();

			// Создание ServiceProvider
			var serviceProvider = serviceCollection.BuildServiceProvider();

			// Получение экземпляра MusicPlayer и выполнение его метода PlayMusic
			var musicPlayer = serviceProvider.GetRequiredService<MusicPlayer>();
			musicPlayer.PlayMusic();
		}
	}

}
