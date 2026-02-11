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
			Kettő = 2,
			Három = 3,
			Négy = 4,
			Öt = 5,
			Hat = 6,
			Hét = 7,
			Nyolc = 8,
			Kilenc = 9,
			Tíz = 10,
			Király = 10,
			Bubi = 10,
			Dáma = 10,
			Ász = 11
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
