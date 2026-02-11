namespace Bunbarlang
{
	internal class Player
	{
        private decimal balance;
		private List<Kartya> pakli;

		private static Random rnd = new Random();

		public Player( decimal startingBalance, List<Kartya> pakli)
		{
			Balance = startingBalance;
			this.pakli = pakli;
		}
        public decimal Balance { get => balance; set => balance = value; }
		public List<Kartya> Pakli { get => pakli; set => pakli = value; }

		public bool PlaceBet(decimal amount)
		{
			if (Balance >= amount)
			{
				Balance -= amount;
				return true;
			}
			else
			{
				Console.WriteLine("Nincs elég pénzed felrakni fogadást!");
				return false;
			}
		}

		public void UpdateBalance(decimal amount)
		{
			Balance += amount;
		}

		public void ShowBalance()
		{
			Console.WriteLine($"Egyenleg: {Balance:C}");
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
				osszeg += (int)item.szam;

				if (item.szam == Kartya.Szam.Asz)
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