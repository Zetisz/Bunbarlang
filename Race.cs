namespace Bunbarlang
{
	internal class Race
	{
		private List<Horse> horses;
		private Pool bettingPool;
		private Track raceTrack;
		private Logger logger;

		// A konstruktor most már átadja a Logger példányt a Pool-nak
		public Race(List<Horse> horses, Logger logger)
		{
			this.horses = horses;
			this.logger = logger;
			bettingPool = new Pool(logger);
			raceTrack = new Track(logger);
		}

		public Horse StartRace()
		{
			Horse winner = raceTrack.StartRace(horses);
			return winner;
		}

        public void Reset(List<Horse> horses)
        {
            foreach (Horse horse in horses)
            {
                horse.DistanceCovered = 0;
            }

			bettingPool.ResetBet();
        }
	}
}
