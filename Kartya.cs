namespace Bunbarlang
{
	internal class Kartya
	{

		public enum Szin
		{
			Pikk,
			Kör,
			Treff,
			Káró
		}

		public enum Szam
		{
			Kettő = 2, Három, Négy, Öt, Hat, Hét, Nyolc, Kilenc, Tíz,
			Bubi, Dáma, Király, Ász
		}
		
		public Szin szin { get; } 
		public Szam szam { get; } 
		public int Ertek { get; }
		public Kartya(Szin szin, Szam szam) 
		{ 
			this.szin = szin; 
			this.szam = szam; 
			this.Ertek = PontErtek(szam);
		}        
		
		// Kártya értékének visszaadása
		public int PontErtek(Szam szam)
		{
			int pont = (int)szam;
			if ((int)szam >= 11 && (int)szam <= 13) pont = 10; // Bubi, Dáma, Király
			if ((int)szam == 14) pont = 11; // Ász
			return pont;
		}
		public override string ToString() 
		{ 
			return $"{szin}-{szam} ({Ertek})"; 
		}
		public static List<Kartya> PakliLetrehozas()
		{
			List<Kartya> pakli = new List<Kartya>();
			foreach (Szin color in Enum.GetValues(typeof(Szin)))
			{
				foreach (Szam num in Enum.GetValues(typeof(Szam)))
				{
					pakli.Add(new Kartya(color, num));
				}
			}
			return pakli;
		}

	}
}
