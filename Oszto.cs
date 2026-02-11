using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunbarlang
{
	internal class Oszto
	{

		private List<Kartya> pakli;

		private static Random rnd = new Random();

		public Oszto(List<Kartya> pakli)
		{
			this.pakli = pakli;
		}

		public List<Kartya> Pakli { get => pakli; set => pakli = value; }

		public static List<Kartya> Osztas(List<Kartya> pakli)
		{
			List<Kartya> osztoKartyai = new List<Kartya>();
			for (int i = 0; i < 2; i++) { 
				Kartya k = pakli[rnd.Next(pakli.Count)];	
				osztoKartyai.Add(k);
                pakli.Remove(k);
			}
			return osztoKartyai;
		}

        public static Kartya LapKeres(List<Kartya> pakli) 
        {
            Kartya lap = pakli[rnd.Next(pakli.Count)];
            pakli.Remove(lap);
            return lap;
        } 

		public static int LapOsszeg(List<Kartya> osztoKartyai, bool mutat) 
        {
            int osszeg = 0;
            int aszDb =  0;

            if (mutat)
            {
	            foreach (var item in osztoKartyai)
	            {
		            osszeg += (int)item.szam;

		            if (item.szam == Kartya.Szam.Ász) {
			            aszDb++; 
		            }
	            }
            }
            else
            {
	            osszeg = (int)osztoKartyai[0].szam;
	            if (osztoKartyai[0].szam == Kartya.Szam.Ász) {
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
