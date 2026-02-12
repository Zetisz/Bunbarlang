namespace Bunbarlang
{
	internal class Program
	{
		private static void Main()
		{
			Logger logger = new();
			Pool bettingTable = new(logger);
			List<Kartya> pakli = Kartya.PakliLetrehozas();
			Player player = new(1000m, pakli);

			bool playAgain = true;
			
			logger.LogColorfulHashtags(10);
			logger.Log("\nÜdv a BŰNBARLANGBAN!", ConsoleColor.Red);

			while (playAgain)
			{
				int cmd;
				player.ShowBalance();
				do
				{
					logger.Log("\nVálassz egy játékot: (1) Lóverseny (2) Blackjack (3) Kilépés",  ConsoleColor.Cyan);
					cmd = int.Parse(Console.ReadLine()!);
				} while (cmd < 1 || cmd > 3);
				
				switch (cmd)
				{
					case 1:
						playAgain = HorseRacing.Game(player, logger, bettingTable);
						break;
					case 2:
						playAgain = BlackjackGame.Game(player, pakli, logger, bettingTable);
						break;
					case 3:
						playAgain = false;
						break;
				}
			}
		}
	}
}
