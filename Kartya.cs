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
			Kor,
			Treff,
			Karo
		}

		public enum Szam
		{
			Ketto = 2,
			Harom = 3,
			Negy = 4,
			Ot = 5,
			Hat = 6,
			Het = 7,
			Nyolc = 8,
			Kilenc = 9,
			Tiz = 10,
			Kiraly = 10,
			Bubi = 10,
			Dama = 10,
			Asz = 11
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
