namespace Bunbarlang
{
	internal class Track
	{
		private Logger logger;

		// Konstruktor, hogy a Track osztály egy Logger-t kapjon
		public Track(Logger logger)
		{
			this.logger = logger;
		}

		public Horse StartRace(List<Horse> horses)
		{
			bool raceOver = false;
			Random rand = new();

			// A verseny folyamatos futtatása
			while (!raceOver)
			{
				// A lovak egyesével futnak
				foreach (var horse in horses)
				{
					horse.Run();

					// Kiírjuk a ló előrehaladását színes hashtagekkel
					PrintRaceProgress(horses, rand);

					// Ellenőrizzük, hogy valamelyik ló elérte a célvonalat
					if (horse.DistanceCovered >= 100)
					{
						raceOver = true;
						logger.LogEndRace(horse); // Nyertes ló naplózása
						break;
					}

					// Kisebb szünetet tartunk, hogy a konzolon élő versenyt lássuk
					Thread.Sleep(200);
				}
			}

			return horses.FirstOrDefault(horse => horse.DistanceCovered >= 100);
		}

		private void PrintRaceProgress(List<Horse> horses, Random rand)
		{
			// Kiürítjük a konzolt minden új iteráció előtt
			Console.Clear();

			// Kiírjuk az összes ló előrehaladását
			foreach (var horse in horses)
			{
				// Színes hashtagek kiírása a ló előrehaladásának megfelelően
				Console.ForegroundColor = GetHorseColor(horse);  // Szín a ló alapján
				Console.Write(new string('#', (int)horse.DistanceCovered));  // A ló előrehaladása
				Console.ResetColor();
				Console.WriteLine($" {horse.Name} - {horse.DistanceCovered:0.0}m");
			}
		}

		private ConsoleColor GetHorseColor(Horse horse)
		{
			// Színek lovakhoz, hogy minden ló más színű legyen
			switch (horse.Color.ToLower())
			{
				case "red":
					return ConsoleColor.Red;
				case "blue":
					return ConsoleColor.Blue;
				case "yellow":
					return ConsoleColor.Yellow;
				case "green":
					return ConsoleColor.Green;
				default:
					return ConsoleColor.White;
			}
		}
	}
}
