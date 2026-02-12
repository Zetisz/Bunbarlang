namespace Bunbarlang
{
    internal class BlackjackGame
    {
        public static bool Game(Player player, List<Kartya> pakli, Logger logger, Pool bettingTable)
        {

            bool playAgain = true;
            List<Kartya> jatekosKartyai = new List<Kartya>();
            List<Kartya> osztoKartyai = new List<Kartya>();
 
            while (playAgain)
            {
				bool jatekosMegall = false;
				bool jatekosBust = false;
				string userResponse;
				jatekosKartyai.Clear();
				osztoKartyai.Clear();
				logger.LogColorfulHashtags(5);

				// Játékos fogadása
                player.ShowBalance();
                var betAmount = player.Fogadas(player);

				// Fogadás elhelyezése
				Bet bet = new(betAmount);
				bettingTable.BJ_AddBet(bet);

				//Játékos lapjai
				
				logger.LogColorfulHashtags(3);
				jatekosKartyai = Player.Osztas(pakli);
				logger.Log("--------Játékos kártyái----------", ConsoleColor.Blue);
				foreach (Kartya k in jatekosKartyai)
				{
					Console.WriteLine(k);
				}
				Console.WriteLine($"\nJátékos lapjainak értéke: {Player.LapOsszeg(jatekosKartyai)}");
 
	            // Osztó lapjai
	            
	            logger.LogColorfulHashtags(3);
				osztoKartyai = Oszto.Osztas(pakli);
				logger.Log("--------Osztó kártyái----------", ConsoleColor.DarkGreen);
				Console.WriteLine(osztoKartyai[0]);
				Console.WriteLine("[?????]");
                Console.WriteLine($"\nOsztó lapjainak értéke: {Oszto.LapOsszeg(osztoKartyai, false)}");


                while (!jatekosMegall && Player.LapOsszeg(jatekosKartyai) < 21)
				{
				    logger.Log("\nAkarsz lapot húzni Igen(1) Nem(2)", ConsoleColor.Cyan);
				    int input = Convert.ToInt32(Console.ReadLine()); 
				    logger.LogColorfulHashtags(3);
				    
				    if (input == 1)
				    {
				        Console.WriteLine("\n--------Lap húzás----------");
				        Kartya ul = Player.LapKeres(pakli);
				        jatekosKartyai.Add(ul);
				        Console.WriteLine(ul);
				        int currentTotal = Player.LapOsszeg(jatekosKartyai);
				        Console.WriteLine($"\nJátékos lapjainak értéke: {currentTotal}");

				        if (currentTotal > 21)
				        {
				            jatekosBust = true;
				            break; // loop kilépés
				        }
				    }
				    else if (input == 2)
				    {
				        jatekosMegall = true;
				    }
				}

				// csak akkor megy ha nem > 21
				if (!jatekosBust)
				{
				    // Oszto

				    Console.WriteLine($"\nOsztó rejtett kártyája: {osztoKartyai[1]}");
				    while (Oszto.LapOsszeg(osztoKartyai, true) < 17)
				    {
					    logger.LogColorfulHashtags(3);
				        Console.WriteLine("\n-----------Osztó lapot húz-----------");
				        Kartya ujLap = Oszto.LapKeres(pakli);
				        osztoKartyai.Add(ujLap);
				        Console.WriteLine("Osztó húz: " + ujLap);
				    }

				    int jatekosPont = Player.LapOsszeg(jatekosKartyai);
				    int osztoPont = Oszto.LapOsszeg(osztoKartyai, true);

				    Console.WriteLine($"\nOsztó lapjainak értéke: {osztoPont}");
				    logger.LogColorfulHashtags(3);

				    // Nyerés logika
				    if (osztoPont > 21 || jatekosPont > osztoPont)
				    {
				        logger.Log("Nyertél!!!",  ConsoleColor.Green);
				        bettingTable.BlackjackPayout(player, true);
				    }
				    else if (jatekosPont < osztoPont)
				    {
				        logger.Log("Vesztettél!!!",   ConsoleColor.Red);
				        bettingTable.BlackjackPayout(player, false);
				    }
				    else
				    {
				        logger.Log("Döntetlen!!!");
				    }
				}
				else
				{
				    logger.Log("Vesztettél!!! (Bust)",  ConsoleColor.Red);
				    bettingTable.BlackjackPayout(player, false);
				}

				// Újrakezdés
				bettingTable.ResetBet();
				userResponse = bettingTable.Restart(player);
				if (userResponse == "exit") return false;
				if (userResponse != "igen" && userResponse != "i") return true;
			}

            return true;
        }
    }
}