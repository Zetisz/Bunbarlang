namespace Bunbarlang
{
	internal class HorseRacing
	{
		public static bool Game(Player player, Logger logger, Pool bettingTable)
		{

			bool bukta = false;
			// Lovak hozzáadása
			List<Horse> horses =
			[
				new("Daiwa Scarlet", 1m, 10, "Red"),
				new("Seiun Sky", 2.5m, 8, "Blue"),
				new("Gold Ship", 1.5m, 9, "Yellow"),
				new("Silence Suzuka", 1m, 10, "Green"),
				new("Special Week", 3m, 8, "White")
			];

			Race race = new(horses, logger);

            // Játék ciklikus indítása
            bool playAgain = true;
			while (playAgain)
			{
				logger.LogColorfulHashtags(5);

				// Fogadás elhelyezése
				player.ShowBalance();
				Console.WriteLine("Melyik lóra szeretnél fogadni? Válassz számot a listából:");

                // Fogadás
                logger.LogHorses(horses);
                var horseChoice = bettingTable.LoValasztas(horses);
				var betAmount = player.Fogadas(player);
				
				Bet bet = new(horses[horseChoice - 1], betAmount);
				bettingTable.HR_AddBet(bet);

                // Verseny indítása
                logger.LogColorfulHashtags(10);
				logger.LogStartRace();
				var winner = race.StartRace();

				// Eredmény kifizetése
				bettingTable.HorsePayout(player, winner);

				player.ShowBalance();
				race.Reset();
				bettingTable.ResetBet();
				string userResponse = bettingTable.Restart(player);
				
				if (userResponse != "igen" && userResponse != "i")
				{
					Console.WriteLine("Köszönjük, hogy játszottál! Viszlát!");
					bukta = true;
					playAgain = false;
				}
				if (userResponse == "exit")
				{
					bukta = false;
					playAgain = false;
				}
			}

			return bukta;
		}
	}
}
