using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public Kartya(Szin szin, Szam szam) 
		{ 
			this.szin = szin; 
			this.szam = szam; 
		}        
			
		//public string SzamString { get => szamString; set => szamString = value; }

		// Kártya értékének visszaadása
		public int kartyaErtek()
		{
			if ((int)szam >= 11 && (int)szam <= 13) return 10; // Bubi, Dáma, Király
			if ((int)szam == 14) return 11; // Ász
			return (int)szam;
		}
		public override string ToString() 
		{ 
			return $"{szin}-{szam}"; 
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
