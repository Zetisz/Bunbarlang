namespace Bunbarlang
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Logger logger = new();
			Pool bettingTable = new(logger);
			List<Kartya> pakli = Kartya.PakliLetrehozas();
			Player player = new(1000m, pakli);
			
			int cmd = 0;
			bool playAgain = true;

			logger.Log("\nÜdv a BŰNBARLANGBAN!", ConsoleColor.Red);
			player.ShowBalance();

			while (playAgain)
			{
				do
				{
					logger.Log("\nVálassz egy játékot: (1) Lóverseny (2) Blackjack (3) Kilépés",  ConsoleColor.Cyan);
					cmd = int.Parse(Console.ReadLine()!);
				} while (cmd < 1 || cmd > 3);


				switch (cmd)
				{
					case 1:
						HorseRacing.Game(player, logger, bettingTable);
						break;
					case 2:
						Blackjack_Game.Game(player, pakli, logger, bettingTable);
						break;
					case 3:
						Environment.Exit(0);
						break;
				}
			}
		}
	}
}
