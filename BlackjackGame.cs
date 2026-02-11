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

				// Játékos fogadása
				decimal betAmount = 0;
                player.ShowBalance();

                do
                {
	                if (betAmount == 0)
		                Console.Write("Mekkora összeget szeretnél fogadni? (Min: 100) ");
	                else
	                {
		                logger.Log(
			                "Érvénytelen összeg. Kérlek, adj meg egy érvényes fogadási összeget, ami nem haladja meg az egyenlegedet.",
			                ConsoleColor.DarkRed);
	                }

	                betAmount = decimal.Parse(Console.ReadLine()!);
                } while (betAmount < 100 || betAmount > player.Balance);

				// Fogadás elhelyezése
				Bet bet = new(betAmount);
				bettingTable.BJ_AddBet(bet);

				//Játékos lapjai
				
				jatekosKartyai = Player.Osztas(pakli);
				logger.Log("--------Játékos kártyái----------", ConsoleColor.Blue);
				foreach (Kartya k in jatekosKartyai)
				{
					Console.WriteLine(k);
				}
				Console.WriteLine($"\nJátékos lapjainak értéke: {Player.LapOsszeg(jatekosKartyai)}");
 
				 // Osztó lapjai
	            
				osztoKartyai = Oszto.Osztas(pakli);
				logger.Log("--------Osztó kártyái----------", ConsoleColor.DarkGreen);
				Console.WriteLine(osztoKartyai[0]);
				Console.WriteLine("[?????]");
                Console.WriteLine($"\nOsztó lapjainak értéke: {Oszto.LapOsszeg(osztoKartyai, false)}");


                while (!jatekosMegall && Player.LapOsszeg(jatekosKartyai) < 21)
				{
				    logger.Log("\nAkarsz lapot húzni Igen(1) Nem(2)", ConsoleColor.Cyan);
				    int input = Convert.ToInt32(Console.ReadLine()); 

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
				            break; // Exit the drawing loop
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
				        Console.WriteLine("\n-----------Osztó lapot húz-----------");
				        Kartya ujLap = Oszto.LapKeres(pakli);
				        osztoKartyai.Add(ujLap);
				        Console.WriteLine("Osztó húz: " + ujLap);
				    }

				    int jatekosPont = Player.LapOsszeg(jatekosKartyai);
				    int osztoPont = Oszto.LapOsszeg(osztoKartyai, true);

				    Console.WriteLine($"\nOsztó lapjainak értéke: {osztoPont}");

				    // Win/Loss Logic
				    if (osztoPont > 21 || jatekosPont > osztoPont)
				    {
				        Console.WriteLine("Nyertél!!!");
				        bettingTable.BlackjackPayout(player, logger, true);
				    }
				    else if (jatekosPont < osztoPont)
				    {
				        Console.WriteLine("Vesztettél!!!");
				        bettingTable.BlackjackPayout(player, logger, false);
				    }
				    else
				    {
				        Console.WriteLine("Döntetlen!!!");
				    }
				}
				else
				{
				    Console.WriteLine("Vesztettél!!! (Bust)");
				    bettingTable.BlackjackPayout(player, logger, false);
				}

				// Global Restart logic (only once at the bottom)
				bettingTable.ResetBet();
				userResponse = bettingTable.Restart(player);
				if (userResponse == "exit") return false;
				if (userResponse != "igen" && userResponse != "i") return true;
			}

            return true;
        }
    }
}