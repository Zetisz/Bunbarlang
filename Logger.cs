using System;

namespace Bunbarlang
{
	internal class Logger
	{
		public void Log(string message, ConsoleColor color = ConsoleColor.White)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(message);
			Console.ResetColor();
		}

		public void LogStartRace()
		{
			Log("A verseny kezdődött!", ConsoleColor.Cyan);
		}

		public void LogEndRace(Horse winner)
		{
			Log($"{winner.Name} megnyerte a versenyt!", ConsoleColor.Green);
		}

		public void HR_LogBetPlaced(string horseName, decimal amount, ConsoleColor color = ConsoleColor.Yellow)
		{
			Log($"Rátettél {amount:C}-ot {horseName}-ra/re.", color);
		}

		public void BJ_LogBetPlaced(decimal amount, ConsoleColor color = ConsoleColor.Yellow)
		{
			Log($"Rátettél {amount:C}-ot.", color);
		}

		public void HR_LogBetResult(string horseName, decimal payout, bool isWin)
		{
			if (isWin)
			{
				Log($"Nyertél {payout:C}, {horseName}-on!", ConsoleColor.Green);
			}
			else
			{
				Log($"Elvesztetted a fogadásod.", ConsoleColor.Red);
			}
		}

		public void BJ_LogBetResult(decimal payout, bool isWin)
		{
			if (isWin)
			{
				Log($"Nyertél {payout:C}", ConsoleColor.Green);
			}
			else
			{
				Log($"Elvesztetted a fogadásod.", ConsoleColor.Red);
			}
		}

		public void LogHorses(List<Horse> horses)
		{
			for (int i = 0; i < horses.Count; i++)
			{
				string color = horses[i].Color;

				if (Enum.TryParse(color, true, out ConsoleColor consoleColor))
				{
					Log($"{i + 1}. {horses[i].Name}", consoleColor);
				}
				else
				{
					Log($"{i + 1}. {horses[i].Name}");
				}
			}
		}

		public void LogColorfulHashtags(int num)
		{
			string[] colors = new string[] { "Cyan", "Magenta", "Yellow", "Green", "Red" };
			Random rand = new Random();

			for (int i = 0; i < num; i++) // amennyit megadunk
			{
				string color = colors[rand.Next(colors.Length)];
				Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color);
				Console.Write("# ");
				Thread.Sleep(150); // lassítjuk a hatást
			}
			Console.ResetColor();
			Console.WriteLine();
		}
	}
}