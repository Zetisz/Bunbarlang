using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunbarlang
{
	internal class HorseRacing
	{
		public static bool Game(Player player, Logger logger, Pool bettingTable)
		{
			

			// Lovak hozzáadása
			List<Horse> horses =
			[
				new Horse("Daiwa Scarlet", 1m, 10, "Red"),
				new Horse("Seiun Sky", 2.5m, 8, "Blue"),
				new Horse("Gold Ship", 1.5m, 9, "Yellow"),
				new Horse("Silence Suzuka", 1m, 10, "Green"),
				new Horse("Special Week", 3m, 8, "White")
			];

			Race race = new(horses, logger);

            // Játék ciklikus indítása
            bool playAgain = true;
			while (playAgain)
			{
				logger.LogColorfulHashtags();

				// Fogadás elhelyezése
				player.ShowBalance();

				Console.WriteLine("Melyik lóra szeretnél fogadni? Válassz számot a listából:");

                // Lovak kiírása számozva
                for (int i = 0; i < horses.Count; i++)
                {
                    string color = horses[i].Color;

                    if (Enum.TryParse(color, true, out ConsoleColor consoleColor))
                    {
                        logger.Log($"{i + 1}. {horses[i].Name}", consoleColor);
                    }
                    else
                    {
                        // fallback color if parsing fails
                        logger.Log($"{i + 1}. {horses[i].Name}", ConsoleColor.White);
                    }
                }

                // Fogadás helyének kiválasztása
                int horseChoice;
				while (true)
				{
					Console.Write("Add meg a választott ló számát (1-5): ");
					if (int.TryParse(Console.ReadLine(), out horseChoice) && horseChoice >= 1 && horseChoice <= horses.Count)
					{
						break; // Ha érvényes számot adott meg
					}
					Console.WriteLine("Érvénytelen választás. Kérlek, válassz a listából egy számot (1-5).");
				}

				// Játékos fogadása
				decimal betAmount;
				while (true)
				{
					Console.Write("Mekkora összeget szeretnél fogadni? ");
					if (decimal.TryParse(Console.ReadLine(), out betAmount) && betAmount <= player.Balance && betAmount > 0)
					{
						break; // Érvényes fogadási összeg
					}
					Console.WriteLine("Érvénytelen összeg. Kérlek, adj meg egy érvényes fogadási összeget, ami nem haladja meg az egyenlegedet.");
				}

				// Fogadás elhelyezése
				Bet bet = new(horses[horseChoice - 1], betAmount);
				bettingTable.BJ_AddBet(bet);

                // Verseny indítása
                logger.LogColorfulHashtags();
				logger.LogStartRace();
				Horse winner = race.StartRace();

				// Eredmény kifizetése
				bettingTable.HorsePayout(player, winner, logger);

				player.ShowBalance();
				race.Reset(horses);
				bettingTable.ResetBet();
				string userResponse = bettingTable.Restart(player);
				
				if (userResponse != "igen" && userResponse != "i")
				{
					Console.WriteLine("Köszönjük, hogy játszottál! Viszlát!");
					return true;
				}
				else
				{
					return false;
				}
			}

			return true;
		}
	}
}
