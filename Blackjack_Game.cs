namespace Bunbarlang
{
    internal class Blackjack_Game
    {
        public static void Game(Player player, List<Kartya> pakli, Logger logger, Pool bettingTable)
        {
            /*foreach (Kartya k in pakli)
            {
                Console.WriteLine(k);
            }*/
			//Console.WriteLine("--------------------------------");
 
            bool playAgain = true;
 
            while (playAgain)
            {

				bool isWin = false;
				bool jatekosMegall = false;
				bool isDraw = false;
				string userResponse;

				// Játékos fogadása
				decimal betAmount;
                player.ShowBalance();

				while (true)
				{
					Console.Write("Mekkora összeget szeretnél fogadni? ");
					if (decimal.TryParse(Console.ReadLine(), out betAmount) && betAmount <= player.Balance && betAmount > 0)
					{
						break; // Érvényes fogadási összeg
					}
					logger.Log("Érvénytelen összeg. Kérlek, adj meg egy érvényes fogadási összeget, ami nem haladja meg az egyenlegedet.", ConsoleColor.DarkRed);
				}

				// Fogadás elhelyezése
				Bet bet = new(betAmount);
				bettingTable.BJ_AddBet(bet);

				//Játékos lapjai
				List<Kartya> jatekosKartyai = new List<Kartya>();
				jatekosKartyai = Player.Osztas(pakli);
                int jatekosLapErtek = Player.LapOsszeg(jatekosKartyai);
				logger.Log("--------Játékos kártyái----------", ConsoleColor.Blue);
				foreach (Kartya k in jatekosKartyai)
				{
					Console.WriteLine(k);
				}
				Console.WriteLine($"\nJátékos lapjainak értéke: {Player.LapOsszeg(jatekosKartyai)}");
 
				 // Osztó lapjai
 
				List<Kartya> osztoKartyai = new List<Kartya>();
				osztoKartyai = Oszto.Osztas(pakli);
                int osztoLapErtek = Oszto.LapOsszeg(osztoKartyai);
				logger.Log("--------Osztó kártyái----------", ConsoleColor.DarkGreen);
				foreach (Kartya k in osztoKartyai)
				{
					Console.WriteLine(k);
				}
                Console.WriteLine($"\nOsztó lapjainak értéke: {Oszto.LapOsszeg(osztoKartyai)}");


                while(!jatekosMegall && Player.LapOsszeg(jatekosKartyai) < 21)
                {
                    logger.Log("\nAkarsz lapot húzni Igen(1) Nem(2)", ConsoleColor.Cyan);
                    int input = Convert.ToInt32(Console.ReadLine());
                    if (input == 1)
                    {
                        // Lap húzás
                        Console.WriteLine("\n--------Lap húzás----------");
                        Kartya ul = Player.LapKeres(pakli);
                        jatekosKartyai.Add(ul);
                        Console.WriteLine(ul);
                        Console.WriteLine($"\nJátékos lapjainak értéke: {Player.LapOsszeg(jatekosKartyai)}");
                        if (21 <= Player.LapOsszeg(jatekosKartyai))
                        {
	                        Console.WriteLine("Vesztettél!!!");
	                        bettingTable.BlackjackPayout(player, logger, false);
	                        
	                        userResponse = bettingTable.Restart(player);
	                        if (userResponse != "igen" && userResponse != "i")
	                        {
		                        Console.WriteLine("Köszönjük, hogy játszottál! Viszlát!");
		                        return;
	                        }
	                        else
	                        {
		                        Environment.Exit(0);
	                        }
                        }
    
                    }
                    else if (input == 2)
                    {
                        Console.WriteLine("--------Játékos kártyái----------");
                        foreach (Kartya k in jatekosKartyai)
                        {
                            Console.WriteLine(k);
                        }
                        Console.WriteLine($"\nJátékos lapjainak értéke: {Player.LapOsszeg(jatekosKartyai)}");
                        jatekosMegall = true;                    
                    }
                }
				//Osztó lapjai
                while(Oszto.LapOsszeg(osztoKartyai) < 17) {
                    Console.WriteLine("\n-----------Osztó lapot húz-----------");
                    Kartya uj_lap = Oszto.LapKeres(pakli);
                    osztoKartyai.Add(uj_lap);
                    Console.WriteLine("Osztó húz: " + uj_lap);
				}
                Console.WriteLine($"\nOsztó lapjainak értéke: {Oszto.LapOsszeg(osztoKartyai)}");

				if ((Player.LapOsszeg(jatekosKartyai) > 21) && (Oszto.LapOsszeg(osztoKartyai) > 21))
				{
					isDraw = true;
				}
                else if (Player.LapOsszeg(jatekosKartyai) > 21)
                {
					isWin = false;
                }
                else if (Oszto.LapOsszeg(osztoKartyai) > 21)
                {
                    isWin = true;
                }
                else if (Player.LapOsszeg(jatekosKartyai) > Oszto.LapOsszeg(osztoKartyai))
                {
                    isWin = true;
                }
                else if (Player.LapOsszeg(jatekosKartyai) < Oszto.LapOsszeg(osztoKartyai))
                {
					isWin = false;
                }
				else
				{
					isDraw = true;
				}

				if (isDraw)
				{
					Console.WriteLine("Döntetlen!!!");
				}
				else if (isWin)
				{
					Console.WriteLine("Nyertél!!!");
					bettingTable.BlackjackPayout(player, logger, true);
				}
				else
				{
					Console.WriteLine("Vesztettél!!!");
					bettingTable.BlackjackPayout(player, logger, false);
				}

				bettingTable.ResetBet();

				// Kérdés, hogy újra akar játszani
				userResponse = bettingTable.Restart(player);
				if (userResponse != "igen" && userResponse != "i")
				{
					Console.WriteLine("Köszönjük, hogy játszottál! Viszlát!");
					return;
				}
				else
				{
					Environment.Exit(0);
				}
			}
            
		}
    }
}