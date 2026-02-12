namespace Bunbarlang
{
	internal class Player
	{
        private decimal balance;
		private Logger logger;
        private static Random rnd = new Random();

		public Player( decimal startingBalance, List<Kartya> pakli)
		{
			this.Balance = startingBalance;
			this.logger = new Logger();
			this.Pakli = pakli;
		}
        public decimal Balance { get => balance; set => balance = value; }
		public List<Kartya> Pakli { get; set; }

		public void ShowBalance()
		{
			logger.Log($"Egyenleg: {Balance:C}");
		}

		public decimal Fogadas(Player player)
		{
			decimal betAmount = 0;
			do
			{
				if (betAmount == 0)
					Console.Write("Mekkora összeget szeretnél fogadni? (Min: 100) ");
				else
				{
					logger.Log("Érvénytelen összeg. Kérlek, adj meg egy érvényes fogadási összeget, ami nem haladja meg az egyenlegedet.", ConsoleColor.DarkRed);
				}

				betAmount = decimal.Parse(Console.ReadLine()!);
			} while (betAmount < 100 || betAmount > player.Balance);
			
			return betAmount;
		}

		public static List<Kartya> Osztas(List<Kartya> pakli)
		{
			List<Kartya> jatekosKartyai = new List<Kartya>();
			for (int i = 0; i < 2; i++)
			{
				Kartya k = pakli[rnd.Next(pakli.Count)];
				jatekosKartyai.Add(k);
				pakli.Remove(k);
			}
			return jatekosKartyai;
		}

		public static Kartya LapKeres(List<Kartya> pakli)
		{
			Kartya lap = pakli[rnd.Next(pakli.Count)];
			pakli.Remove(lap);
			return lap;
		}

		public static int LapOsszeg(List<Kartya> jatekosKartyai)
		{
			int osszeg = 0;
			int aszDb = 0;
			foreach (var item in jatekosKartyai)
			{
				osszeg += item.Ertek;

				if (item.szam == Kartya.Szam.Ász)
				{
					aszDb++;
				}
			}

			while (osszeg > 21 && aszDb > 0)
			{
				osszeg -= 10;
				aszDb--;
			}

			return osszeg;
		}
	}

}